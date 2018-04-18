using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LanguageExt;
using Newtonsoft.Json.Linq;
using Unit = System.Reactive.Unit;

namespace ExRam.Gremlinq
{
    public static class GremlinQueryProvider
    {
        private sealed class JsonSupportTypedGremlinQueryProvider : ITypedGremlinQueryProvider
        {
            private static readonly JArray EmptyJArray = new JArray();
            private static readonly GraphsonDeserializer Serializer = new GraphsonDeserializer();

            private readonly JsonTransformRule _transformRule;
            private readonly IModelGremlinQueryProvider<JToken> _baseProvider;

            public JsonSupportTypedGremlinQueryProvider(IModelGremlinQueryProvider<JToken> baseProvider)
            {
                this._baseProvider = baseProvider;

                this._transformRule = JsonTransformRules
                    .Empty
                    .Lazy((token, recurse) =>
                    {
                        if (token is JObject jObject && jObject.Parent?.Parent?.Parent?.Parent is JProperty parentProperty && parentProperty.Name.Equals("properties", StringComparison.OrdinalIgnoreCase))
                        {
                            return jObject
                                .TryGetValue("value")
                                .Bind(recurse);
                        }

                        return Option<JToken>.None;
                    })
                    .Lazy((token, recurse) =>
                    {
                        if (token is JObject jObject && !jObject.ContainsKey("$type"))
                        {
                            return jObject
                                .TryGetValue("label")
                                .Bind(labelToken => baseProvider.Model
                                    .TryGetElementTypeOfLabel(labelToken.ToString()))
                                .Bind(type =>
                                {
                                    jObject.AddFirst(new JProperty("$type", type.AssemblyQualifiedName));

                                    return recurse(jObject);
                                });
                        }

                        return Option<JToken>.None;
                    })
                    .Lazy((token, recurse) =>
                    {
                        if (token is JObject jObject && jObject["properties"] is JObject propertiesObject)
                        {
                            foreach (var item in propertiesObject)
                            {
                                jObject[item.Key] = recurse(item.Value).IfNone(jObject[item.Key]);
                            }

                            propertiesObject.Parent.Remove();

                            return jObject;
                        }

                        return Option<JToken>.None;
                    })
                    .Lazy(JsonTransformRules.Identity);
            }

            public IAsyncEnumerable<TElement> Execute<TElement>(IGremlinQuery<TElement> query)
            {
                return this._baseProvider
                    .Execute(query)
                    .SelectMany(rawData => Serializer
                        .Deserialize<TElement[]>(new JTokenReader(rawData
                            .Transform(this._transformRule)
                            .IfNone(EmptyJArray)))
                        .ToAsyncEnumerable());
            }

            public IGraphModel Model => this._baseProvider.Model;
            
            public IGremlinQuery<Unit> TraversalSource => this._baseProvider.TraversalSource;
        }

        private sealed class ModelGremlinQueryProvider<TNative> : IModelGremlinQueryProvider<TNative>
        {
            private readonly INativeGremlinQueryProvider<TNative> _baseProvider;

            public ModelGremlinQueryProvider(INativeGremlinQueryProvider<TNative> baseProvider, IGraphModel newModel)
            {
                this.Model = newModel;
                this._baseProvider = baseProvider;
            }

            public IAsyncEnumerable<TNative> Execute(IGremlinQuery query)
            {
                var serialized = query
                    .Cast<Unit>()
                    .Resolve(this.Model)
                    .Serialize();

                return this._baseProvider
                    .Execute(serialized.queryString, serialized.parameters);
            }

            public IGraphModel Model { get; }
            public IGremlinQuery<Unit> TraversalSource => this._baseProvider.TraversalSource;
        }

        private sealed class RewriteStepsQueryProvider<TStep, TNative> : IModelGremlinQueryProvider<TNative> where TStep : NonTerminalGremlinStep
        {
            private readonly IModelGremlinQueryProvider<TNative> _baseTypedGremlinQueryProvider;
            private readonly Func<TStep, Option<IEnumerable<GremlinStep>>> _replacementStepFactory;

            public RewriteStepsQueryProvider(IModelGremlinQueryProvider<TNative> baseTypedGremlinQueryProvider, Func<TStep, Option<IEnumerable<GremlinStep>>> replacementStepFactory)
            {
                this._replacementStepFactory = replacementStepFactory;
                this._baseTypedGremlinQueryProvider = baseTypedGremlinQueryProvider;
            }

            public IAsyncEnumerable<TNative> Execute(IGremlinQuery query)
            {
                return this._baseTypedGremlinQueryProvider.Execute(query
                    .Cast<Unit>()
                    .RewriteSteps(step => step is TStep replacedStep
                        ? this._replacementStepFactory(replacedStep)
                        : Option<IEnumerable<GremlinStep>>.None)
                    .Cast<Unit>());
            }

            public IGraphModel Model => this._baseTypedGremlinQueryProvider.Model;

            public IGremlinQuery<Unit> TraversalSource => this._baseTypedGremlinQueryProvider.TraversalSource;
        }
        
        private sealed class SelectNativeGremlinQueryProvider<TNativeSource, TNativeTarget> : INativeGremlinQueryProvider<TNativeTarget>
        {
            private readonly Func<TNativeSource, TNativeTarget> _projection;
            private readonly INativeGremlinQueryProvider<TNativeSource> _provider;

            public SelectNativeGremlinQueryProvider(INativeGremlinQueryProvider<TNativeSource> provider, Func<TNativeSource, TNativeTarget> projection)
            {
                this._provider = provider;
                this._projection = projection;
            }

            public IAsyncEnumerable<TNativeTarget> Execute(string query, IDictionary<string, object> parameters)
            {
                return this._provider
                    .Execute(query, parameters)
                    .Select(this._projection);
            }

            public IGremlinQuery<Unit> TraversalSource => this._provider.TraversalSource;
        }

        public static IAsyncEnumerable<TElement> Execute<TElement>(this IGremlinQuery<TElement> query, ITypedGremlinQueryProvider provider)
        {
            return provider.Execute(query);
        }

        public static ITypedGremlinQueryProvider WithJsonSupport(this IModelGremlinQueryProvider<JToken> provider)
        {
            return new JsonSupportTypedGremlinQueryProvider(provider);
        }

        public static IModelGremlinQueryProvider<TNative> WithModel<TNative>(this INativeGremlinQueryProvider<TNative> provider, IGraphModel model)
        {
            return new ModelGremlinQueryProvider<TNative>(provider, model);
        }
       
        public static IModelGremlinQueryProvider<TNative> ReplaceElementProperty<TSource, TProperty, TNative>(this IModelGremlinQueryProvider<TNative> provider, Func<TSource, bool> overrideCriterion, Expression<Func<TSource, TProperty>> memberExpression, TProperty value)
        {
            return provider
                .DecorateElementProperty(overrideCriterion, step => new ReplaceElementPropertyStep<TSource, TProperty>(step, memberExpression, value));
        }

        public static IModelGremlinQueryProvider<TNative> SetDefautElementProperty<TSource, TProperty, TNative>(this IModelGremlinQueryProvider<TNative> provider, Func<TSource, bool> overrideCriterion, Expression<Func<TSource, TProperty>> memberExpression, TProperty value)
        {
            return provider
                .DecorateElementProperty(overrideCriterion, step => new SetDefaultElementPropertyStep<TSource, TProperty>(step, memberExpression, value));
        }

        public static IModelGremlinQueryProvider<TNative> DecorateElementProperty<TSource, TProperty, TNative>(this IModelGremlinQueryProvider<TNative> provider, Func<TSource, bool> overrideCriterion, Func<AddElementPropertiesStep, DecorateAddElementPropertiesStep<TSource, TProperty>> replacementStepFactory)
        {
            return provider
                .RewriteSteps<AddElementPropertiesStep, TNative>(step =>
                {
                    if (step.Element is TSource source)
                    {
                        if (overrideCriterion(source))
                            return new[] { replacementStepFactory(step) };
                    }

                    return Option<IEnumerable<GremlinStep>>.None;
                });
        }

        public static IModelGremlinQueryProvider<TNative> RewriteSteps<TStep, TNative>(this IModelGremlinQueryProvider<TNative> provider, Func<TStep, Option<IEnumerable<GremlinStep>>> replacementStepFactory) where TStep : NonTerminalGremlinStep
        {
            return new RewriteStepsQueryProvider<TStep, TNative>(provider, replacementStepFactory);
        }

        public static INativeGremlinQueryProvider<TNativeTarget> Select<TNativeSource, TNativeTarget>(this INativeGremlinQueryProvider<TNativeSource> provider, Func<TNativeSource, TNativeTarget> projection)
        {
            return new SelectNativeGremlinQueryProvider<TNativeSource, TNativeTarget>(provider, projection);
        }
    }
}