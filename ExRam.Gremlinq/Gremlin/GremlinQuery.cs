using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using LanguageExt;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ExRam.Gremlinq
{
    public static class GremlinQuery<TElement>
    {
        internal sealed class GremlinQueryImpl<TOutVertex, TInVertex> : IGremlinQuery<TOutVertex, TInVertex, TElement>
        {
            public GremlinQueryImpl(IImmutableList<GremlinStep> steps, IImmutableDictionary<StepLabel, string> stepLabelBindings)
            {
                this.Steps = steps;
                this.StepLabelMappings = stepLabelBindings;
            }

            public IGremlinQuery<TVertex> AddV<TVertex>(TVertex vertex)
            {
                return this
                    .AddStep<TElement, TVertex>(new AddVGremlinStep(vertex))
                    .AddStep(new AddElementPropertiesStep(vertex));
            }

            public IGremlinQuery<TVertex> AddV<TVertex>()
                where TVertex : new()
            {
                return this.AddV(new TVertex());
            }

            public IGremlinQuery<TEdge> AddE<TEdge>()
                where TEdge : new()
            {
                return this.AddE(new TEdge());
            }

            public IGremlinQuery<TEdge> AddE<TEdge>(TEdge edge)
            {
                return this
                    .AddStep<TElement, TEdge>(new AddEGremlinStep(edge))
                    .AddStep(new AddElementPropertiesStep(edge));
            }

            public IGremlinQuery<TElement> And(params Func<IGremlinQuery<TElement>, IGremlinQuery>[] andTraversals)
            {
                return this.AddStep(
                    "and",
                    andTraversals
                        .Select(andTraversal => andTraversal(this.ToAnonymous()))
                        .Aggregate(
                            ImmutableList<object>.Empty,
                            (list, query2) => query2.Steps.Count == 2 && (query2.Steps[1] as MethodGremlinStep)?.Name == "and"
                                ? list.AddRange(((MethodGremlinStep)query2.Steps[1]).Parameters)
                                : list.Add(query2)));
            }

            public IGremlinQuery<TTarget> As<TTarget>(Func<IGremlinQuery<TElement>, StepLabel<TElement>, IGremlinQuery<TTarget>> continuation)
            {
                var stepLabel = new StepLabel<TElement>();

                return continuation(
                    this.As(stepLabel),
                    stepLabel);
            }

            public IGremlinQuery<TElement> As(StepLabel<TElement> stepLabel)
            {
                return this
                    .AddStep("as", stepLabel);
            }

            public IGremlinQuery<TElement> Barrier()
            {
                return this
                    .AddStep("barrier");
            }

            IGremlinQuery<TTarget> IGremlinQuery.Cast<TTarget>()
            {
                return this.Cast<TTarget>();
            }

            public IGremlinQuery<TOutVertex, TInVertex, TTarget> Cast<TTarget>()
            {
                return new GremlinQuery<TTarget>.GremlinQueryImpl<TOutVertex, TInVertex>(this.Steps, this.StepLabelMappings);
            }

            public IGremlinQuery<TTarget, TInVertex, TElement> CastOutVertex<TTarget>()
            {
                return new GremlinQuery<TElement>.GremlinQueryImpl<TTarget, TInVertex>(this.Steps, this.StepLabelMappings);
            }

            public IGremlinQuery<TOutVertex, TTarget, TElement> CastInVertex<TTarget>()
            {
                return new GremlinQuery<TElement>.GremlinQueryImpl<TOutVertex, TTarget>(this.Steps, this.StepLabelMappings);
            }

            public IGremlinQuery<TTarget> Coalesce<TTarget>(params Func<IGremlinQuery<TElement>, IGremlinQuery<TTarget>>[] traversals)
            {
                return this
                    .AddStep<TElement, TTarget>(
                        "coalesce",
                        traversals
                            .Select(traversal => traversal(this.ToAnonymous()))
                            .ToImmutableList<object>());
            }

            public IGremlinQuery<TResult> Choose<TResult>(Func<IGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IGremlinQuery<TElement>, IGremlinQuery<TResult>> trueChoice, Func<IGremlinQuery<TElement>, IGremlinQuery<TResult>> falseChoice)
            {
                var anonymous = this.ToAnonymous();

                return this
                    .AddStep<TElement, TResult>(
                        "choose",
                        traversalPredicate(anonymous),
                        trueChoice(anonymous),
                        falseChoice(anonymous));
            }

            public IGremlinQuery<TResult> Choose<TResult>(Func<IGremlinQuery<TElement>, IGremlinQuery> traversalPredicate, Func<IGremlinQuery<TElement>, IGremlinQuery<TResult>> trueChoice)
            {
                var anonymous = this.ToAnonymous();

                return this
                    .AddStep<TElement, TResult>(
                        "choose",
                        traversalPredicate(anonymous),
                        trueChoice(anonymous));
            }

            public IGremlinQuery<Vertex> Both()
            {
                return this
                    .AddStep<TElement, Vertex>(new DerivedLabelNamesGremlinStep<TElement>("both"));
            }

            public IGremlinQuery<TEdge> BothE<TEdge>()
            {
                return this
                    .AddStep<TElement, TEdge>(new DerivedLabelNamesGremlinStep<TEdge>("bothE"));
            }

            public IGremlinQuery<Vertex> BothV()
            {
                return this
                    .AddStep<TElement, Vertex>("bothV");
            }

            public IGremlinQuery<TEnd> BranchOnIdentity<TEnd>(params Func<IGremlinQuery<TElement>, IGremlinQuery<TEnd>>[] options)
            {
                return this
                    .Branch(_ => _.Identity(), options);
            }

            public IGremlinQuery<TEnd> Branch<TBranch, TEnd>(Func<IGremlinQuery<TElement>, IGremlinQuery<TBranch>> branchSelector, params Func<IGremlinQuery<TBranch>, IGremlinQuery<TEnd>>[] options)
            {
                return options
                    .Aggregate(
                        this
                            .AddStep<TElement, TBranch>("branch", branchSelector(this.ToAnonymous())),
                        (branchQuery, option) => branchQuery.AddStep("option", option(branchQuery.ToAnonymous())))
                    .Cast<TEnd>();
            }

            public IGremlinQuery<TElement> ByTraversal(Func<IGremlinQuery<TElement>, IGremlinQuery> traversal, Order sortOrder)
            {
                return this
                    .AddStep("by", traversal(this.ToAnonymous()), sortOrder);
            }

            public IGremlinQuery<TElement> ByMember(Expression<Func<TElement, object>> projection, Order sortOrder)
            {
                var body = projection.Body;
                if (body is UnaryExpression && body.NodeType == ExpressionType.Convert)
                    body = ((UnaryExpression)body).Operand;

                if (body is MemberExpression memberExpression)
                {
                    return this
                        .AddStep("by", memberExpression.Member.Name, sortOrder);
                }

                throw new NotSupportedException();
            }

            public IGremlinQuery<TElement> ByLambda(string lambdaString)
            {
                return this
                    .AddStep("by", new Lambda(lambdaString));
            }

            public IGremlinQuery<TElement> Dedup()
            {
                return this
                    .AddStep("dedup");
            }

            public IGremlinQuery<Unit> Drop()
            {
                return this
                    .AddStep<TElement, Unit>("drop");
            }

            public IGremlinQuery<Edge> E(params object[] ids)
            {
                return this
                    .AddStep<TElement, Edge>("E", ids);
            }

            public IGremlinQuery<TElement> Emit()
            {
                return this
                    .AddStep("emit");
            }

            public IGremlinQuery<TElement> Explain()
            {
                return this
                    .AddStep("explain");
            }

            public IGremlinQuery<TElement> FilterWithLambda(string lambda)
            {
                return this
                    .AddStep("filter", new Lambda(lambda));
            }

            public IGremlinQuery<TElement[]> Fold()
            {
                return this
                    .AddStep<TElement, TElement[]>("fold");
            }

            public IGremlinQuery<TElement> From<TStepLabel>(StepLabel<TStepLabel> stepLabel)
            {
                return this
                    .AddStep("from", stepLabel);
            }

            public IGremlinQuery<TElement> From(Func<IGremlinQuery<TElement>, IGremlinQuery> fromVertex)
            {
                return this
                    .AddStep("from", fromVertex(this.ToAnonymous()));
            }

            public IGremlinQuery<object> Id()
            {
                return this
                    .AddStep<TElement, object>("id");
            }

            public IGremlinQuery<TElement> Identity()
            {
                return this
                    .AddStep("identity");
            }

            public IGremlinQuery<Vertex> In<TEdge>()
            {
                return this
                    .AddStep<TElement, Vertex>(new DerivedLabelNamesGremlinStep<TEdge>("in"));
            }

            IGremlinQuery<TTarget> IGremlinQuery.InsertStep<TTarget>(int index, GremlinStep step)
            {
                return this.InsertStep<TTarget>(index, step);
            }

            public IGremlinQuery<TOutVertex, TInVertex, TTarget> InsertStep<TTarget>(int index, GremlinStep step)
            {
                return new GremlinQuery<TTarget>.GremlinQueryImpl<TOutVertex, TInVertex>(this.Steps.Insert(index, step), this.StepLabelMappings);
            }

            public IGremlinQuery<TEdge> InE<TEdge>()
            {
                return this
                    .AddStep<TElement, TEdge>(new DerivedLabelNamesGremlinStep<TEdge>("inE"));
            }

            public IGremlinQuery<TVertex> InV<TVertex>()
            {
                return this
                    .AddStep<TElement, Vertex>("inV")
                    .OfType<TVertex>();
            }

            public IGremlinQuery<TElement> Inject(params TElement[] elements)
            {
                return this
                    .AddStep("inject", elements);
            }

            public IGremlinQuery<TElement> Limit(long limit)
            {
                return this
                    .AddStep("limit", limit);
            }

            public IGremlinQuery<TTarget> Local<TTarget>(Func<IGremlinQuery<TElement>, IGremlinQuery<TTarget>> localTraversal)
            {
                return this
                    .AddStep<TElement, TTarget>("local", localTraversal(this.ToAnonymous()));
            }

            public IGremlinQuery<TTarget> Map<TTarget>(Func<IGremlinQuery<TElement>, IGremlinQuery<TTarget>> mapping)
            {
                return this
                    .AddStep<TElement, TTarget>("map", mapping(this.ToAnonymous()));
            }

            public IGremlinQuery<TElement> Match(params Func<IGremlinQuery<TElement>, IGremlinQuery<TElement>>[] matchTraversals)
            {
                return this
                    // ReSharper disable once CoVariantArrayConversion
                    .AddStep("match", matchTraversals.Select(traversal => traversal(this.ToAnonymous())).ToArray());
            }

            public IGremlinQuery<TElement> Not(Func<IGremlinQuery<TElement>, IGremlinQuery> notTraversal)
            {
                return this
                    .AddStep("not", notTraversal(this.ToAnonymous()));
            }

            public IGremlinQuery<TTarget> OfType<TTarget>()
            {
                return this
                    .Cast<TTarget>()
                    .AddStep(new DerivedLabelNamesGremlinStep<TTarget>("hasLabel"));
            }

            public IGremlinQuery<TTarget> Optional<TTarget>(Func<IGremlinQuery<TElement>, IGremlinQuery<TTarget>> optionalTraversal)
            {
                return this
                    .AddStep<TElement, TTarget>("optional", optionalTraversal(this.ToAnonymous()));
            }

            public IGremlinQuery<TElement> Or(params Func<IGremlinQuery<TElement>, IGremlinQuery>[] orTraversals)
            {
                return this.AddStep(
                    "or",
                    orTraversals
                        .Select(andTraversal => andTraversal(this.ToAnonymous()))
                        .Aggregate(
                            ImmutableList<object>.Empty,
                            (list, query2) => query2.Steps.Count == 2 && (query2.Steps[1] as MethodGremlinStep)?.Name == "or"
                                ? list.AddRange(((MethodGremlinStep)query2.Steps[1]).Parameters)
                                : list.Add(query2)));
            }

            public IGremlinQuery<TElement> Order()
            {
                return this
                    .AddStep("order");
            }

            public IGremlinQuery<Vertex> OtherV()
            {
                return this
                    .AddStep<TElement, Vertex>("otherV");
            }

            public IGremlinQuery<TEdge> OutE<TEdge>()
            {
                return this
                    .AddStep<TElement, TEdge>(new DerivedLabelNamesGremlinStep<TEdge>("outE"));
            }

            public IGremlinQuery<TVertex> OutV<TVertex>()
            {
                return this
                    .AddStep<TElement, Vertex>("outV")
                    .OfType<TVertex>();
            }

            public IGremlinQuery<Vertex> Out<TEdge>()
            {
                return this
                    .AddStep<TElement, Vertex>(new DerivedLabelNamesGremlinStep<TEdge>("out"));
            }

            public IGremlinQuery<string> Profile()
            {
                return this
                    .AddStep<TElement, string>("profile");
            }

            public IGremlinQuery<TElement> Property<TProperty>(Expression<Func<TElement, TProperty>> propertyExpression, TProperty property)
            {
                if (propertyExpression.Body is MemberExpression memberExpression)
                {
                    if (memberExpression.Expression == propertyExpression.Parameters[0])
                    {
                        return this.Property(memberExpression.Member.Name, property);
                    }
                }

                throw new NotSupportedException();
            }

            public IGremlinQuery<TElement> Property(string key, object value)
            {
                return this
                    .AddStep("property", key, value);
            }

            public IGremlinQuery<TElement> Range(long low, long high)
            {
                return this
                    .AddStep("range", low, high);
            }

            public IGremlinQuery<TElement> Repeat(Func<IGremlinQuery<TElement>, IGremlinQuery<TElement>> repeatTraversal)
            {
                return this
                    .AddStep("repeat", repeatTraversal(this.ToAnonymous()));
            }

            public IGremlinQuery<TStep> Select<TStep>(StepLabel<TStep> label)
            {
                return this
                    .AddStep<TElement, TStep>("select", label);
            }

            public IGremlinQuery<(T1, T2)> Select<T1, T2>(StepLabel<T1> label1, StepLabel<T2> label2)
            {
                return this
                    .AddStep<TElement, (T1, T2)>("select", label1, label2)
                    .AddStepLabelBinding(x => x.Item1, label1)
                    .AddStepLabelBinding(x => x.Item2, label2);
            }

            public IGremlinQuery<(T1, T2, T3)> Select<T1, T2, T3>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3)
            {
                return this
                    .AddStep<TElement, (T1, T2, T3)>("select", label1, label2, label3)
                    .AddStepLabelBinding(x => x.Item1, label1)
                    .AddStepLabelBinding(x => x.Item2, label2)
                    .AddStepLabelBinding(x => x.Item3, label3);
            }

            public IGremlinQuery<(T1, T2, T3, T4)> Select<T1, T2, T3, T4>(StepLabel<T1> label1, StepLabel<T2> label2, StepLabel<T3> label3, StepLabel<T4> label4)
            {
                return this
                    .AddStep<TElement, (T1, T2, T3, T4)>("select", label1, label2, label3, label4)
                    .AddStepLabelBinding(x => x.Item1, label1)
                    .AddStepLabelBinding(x => x.Item2, label2)
                    .AddStepLabelBinding(x => x.Item3, label3)
                    .AddStepLabelBinding(x => x.Item4, label4);
            }

            public IGremlinQuery<TElement> SideEffect(Func<IGremlinQuery<TElement>, IGremlinQuery> sideEffectTraversal)
            {
                return this
                    .AddStep("sideEffect", sideEffectTraversal(this.ToAnonymous()));
            }

            public IGremlinQuery<TElement> Skip(long skip)
            {
                return this
                    .AddStep("skip", skip);
            }

            public IGremlinQuery<TElement> Sum(Scope scope)
            {
                return this.AddStep("sum", scope);
            }

            public IGremlinQuery<TElement> Times(int count)
            {
                return this
                    .AddStep("times", count);
            }

            public IGremlinQuery<TElement> Tail(long limit)
            {
                return this
                    .AddStep("tail", limit);
            }

            public IGremlinQuery<TElement> To<TStepLabel>(StepLabel<TStepLabel> stepLabel)
            {
                return this
                    .AddStep("to", stepLabel);
            }

            public IGremlinQuery<TElement> To(Func<IGremlinQuery<TElement>, IGremlinQuery> toVertex)
            {
                return this
                    .AddStep("to", toVertex(this.ToAnonymous()));
            }

            public IGremlinQuery<TElement> Unfold(IGremlinQuery<IEnumerable<TElement>> query)
            {
                return query
                    .AddStep< IEnumerable<TElement>, TElement >("unfold");
            }

            public IGremlinQuery<TTarget> Union<TTarget>(params Func<IGremlinQuery<TElement>, IGremlinQuery<TTarget>>[] unionTraversals)
            {
                return this
                    .AddStep<TElement, TTarget>("union", unionTraversals
                        .Select(unionTraversal => unionTraversal(this.ToAnonymous()))
                        .ToImmutableList<object>());
            }

            public IGremlinQuery<TElement> Until(Func<IGremlinQuery<TElement>, IGremlinQuery> untilTraversal)
            {
                return this
                    .AddStep("until", untilTraversal(this.ToAnonymous()));
            }

            public IGremlinQuery<Vertex> V(params object[] ids)
            {
                return this
                    .AddStep<TElement, Vertex>("V", ids);
            }

            public IGremlinQuery<TTarget> Values<TTarget>(params Expression<Func<TElement, TTarget>>[] projections)
            {
                return this.AddStep<TElement, TTarget>(new ValuesGremlinStep<TElement, TTarget>(projections));
            }

            public IGremlinQuery<TElement> Where(Func<IGremlinQuery<TElement>, IGremlinQuery> filterTraversal)
            {
                return this
                    .AddStep("where", filterTraversal(this.ToAnonymous()));
            }

            public GroovyExpressionState Serialize(StringBuilder stringBuilder, GroovyExpressionState state)
            {
                foreach (var step in this.Steps)
                {
                    if (step is IGroovySerializable serializableStep)
                        state = serializableStep.Serialize(stringBuilder, state);
                    else
                        throw new ArgumentException("Query contains non-serializable step. Please call RewriteSteps on the query first.");
                }

                return state;
            }

            public IImmutableList<GremlinStep> Steps { get; }
            public IImmutableDictionary<StepLabel, string> StepLabelMappings { get; }
        }

        public static readonly IGremlinQuery<TElement> Anonymous = GremlinQuery<TElement>.Create("__");

        public static IGremlinQuery<TElement> Create(string graphName = "g")
        {
            return new GremlinQueryImpl<Unit, Unit>(ImmutableList<GremlinStep>.Empty.Add(new IdentifierGremlinStep(graphName)), ImmutableDictionary<StepLabel, string>.Empty);
        }
    }

    public static class GremlinQuery
    {
        public static readonly IGremlinQuery<Unit> Anonymous = GremlinQuery<Unit>.Anonymous;

        public static IGremlinQuery<Unit> Create(string graphName)
        {
            return GremlinQuery<Unit>.Create(graphName);
        }

        public static IGremlinQuery<TElement> ToAnonymous<TElement>(this IGremlinQuery<TElement> query)
        {
            return GremlinQuery<TElement>.Anonymous;
        }

        public static IGremlinQuery<TElement> SetModel<TElement>(this IGremlinQuery<TElement> query, IGraphModel model)
        {
            return query
                .AddStep(new SetModelGremlinStep(model));
        }

        public static IGremlinQuery<TElement> SetTypedGremlinQueryProvider<TElement>(this IGremlinQuery<TElement> query, ITypedGremlinQueryProvider queryProvider)
        {
            return query
                .AddStep(new SetTypedGremlinQueryProviderGremlinStep(queryProvider));
        }

        public static (string queryString, IDictionary<string, object> parameters) Serialize(this IGremlinQuery query)
        {
            var stringBuilder = new StringBuilder();

            var groovyBuilder = query
                .Serialize(stringBuilder, GroovyExpressionState.FromQuery(query));

            return (stringBuilder.ToString(), groovyBuilder.GetVariables());
        }

        public static Task<TElement> FirstAsync<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            return query
                .Limit(1)
                .Execute()
                .First(ct);
        }

        public static async Task<Option<TElement>> FirstOrNoneAsync<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            var array = await query
                .Limit(1)
                .Execute()
                .ToArray(ct)
                .ConfigureAwait(false);

            return array.Length > 0
                ? array[0]
                : Option<TElement>.None;
        }

        public static Task<TElement[]> ToArrayAsync<TElement>(this IGremlinQuery<TElement> query, CancellationToken ct = default)
        {
            return query
                .Execute()
                .ToArray(ct);
        }

        public static IGremlinQuery<TElement> AddStep<TElement>(this IGremlinQuery<TElement> query, string name, params object[] parameters)
        {
            return query.AddStep(new MethodGremlinStep(name, parameters));
        }

        public static IGremlinQuery<TElement> AddStep<TElement>(this IGremlinQuery<TElement> query, GremlinStep step)
        {
            return query.InsertStep<TElement>(query.Steps.Count, step);
        }

        public static IGremlinQuery<TElement> AddStep<TElement>(this IGremlinQuery<TElement> query, string name, ImmutableList<object> parameters)
        {
            return query.InsertStep<TElement>(query.Steps.Count, new MethodGremlinStep(name, parameters));
        }

        
        public static IGremlinQuery<TTarget> AddStep<TElement, TTarget>(this IGremlinQuery<TElement> query, string name, params object[] parameters)
        {
            return query.AddStep<TElement, TTarget>(new MethodGremlinStep(name, parameters));
        }

        public static IGremlinQuery<TTarget> AddStep<TElement, TTarget>(this IGremlinQuery<TElement> query, GremlinStep step)
        {
            return query.InsertStep<TTarget>(query.Steps.Count, step);
        }

        public static IGremlinQuery<TTarget> AddStep<TElement, TTarget>(this IGremlinQuery<TElement> query, string name, ImmutableList<object> parameters)
        {
            return query.InsertStep<TTarget>(query.Steps.Count, new MethodGremlinStep(name, parameters));
        }

        
        public static IGremlinQuery<TEdge> E<TEdge>(this IGremlinQuery query, params object[] ids)
        {
            return query
                .E(ids)
                .OfType<TEdge>();
        }

        public static IGremlinQuery<TElement> ReplaceSteps<TElement>(this IGremlinQuery<TElement> query, IImmutableList<GremlinStep> steps)
        {
            return ReferenceEquals(steps, query.Steps) 
                ? query 
                : new GremlinQuery<TElement>.GremlinQueryImpl<Unit, Unit>(steps, query.StepLabelMappings);
        }

        public static IGremlinQuery<TElement> Resolve<TElement>(this IGremlinQuery<TElement> query)
        {
            var model = query
                .TryGetModel()
                .IfNone(() => throw new ArgumentException("No IGraphModel set on the query."));

            return query
                .RewriteSteps(x => Option<IEnumerable<GremlinStep>>.Some(x.Resolve(model)))
                .Cast<TElement>();
        }

        public static IGremlinQuery<Unit> RewriteSteps(this IGremlinQuery query, Func<NonTerminalGremlinStep, Option<IEnumerable<GremlinStep>>> resolveFunction)
        {
            var steps = query.Steps;

            for(var index = 0; index < steps.Count; index++)
            {
                var step = steps[index];

                switch (step)
                {
                    case MethodGremlinStep terminal:
                    {
                        var parameters = terminal.Parameters;

                        for (var j = 0; j < parameters.Count; j++)
                        {
                            var parameter = parameters[j];

                            if (parameter is IGremlinQuery subQuery)
                                parameters = parameters.SetItem(j, subQuery.RewriteSteps(resolveFunction));
                        }

                        if (!object.ReferenceEquals(parameters, terminal.Parameters))
                            steps = steps.SetItem(index, new MethodGremlinStep(terminal.Name, parameters));

                        break;
                    }
                    case NonTerminalGremlinStep nonTerminal:
                    {
                        var newTuple = resolveFunction(nonTerminal)
                            .Fold(
                                (steps, index),
                                (tuple, newSteps) => (
                                    tuple.steps
                                        .RemoveAt(tuple.index)
                                        .InsertRange(tuple.index, newSteps),
                                    tuple.index - 1));

                        index = newTuple.index;
                        steps = newTuple.steps;

                        break;
                    }
                }
            }

            return query
                .Cast<Unit>()
                .ReplaceSteps(steps);
        }

        public static IGremlinQuery<TVertex> V<TVertex>(this IGremlinQuery query, params object[] ids)
        {
            return query
                .V(ids)
                .OfType<TVertex>();
        }

        internal static IGremlinQuery<TElement> AddStepLabelBinding<TElement>(this IGremlinQuery<TElement> query, Expression<Func<TElement, object>> memberExpression, StepLabel stepLabel)
        {
            var body = memberExpression.Body.StripConvert();
            
            if (!(body is MemberExpression memberExpressionBody))
                throw new ArgumentException();

            return new GremlinQuery<TElement>.GremlinQueryImpl<Unit, Unit>(query.Steps, query.StepLabelMappings.SetItem(stepLabel, memberExpressionBody.Member.Name));
        }

        internal static IGremlinQuery<TElement> ReplaceProvider<TElement>(this IGremlinQuery<TElement> query, ITypedGremlinQueryProvider provider)
        {
            return new GremlinQuery<TElement>.GremlinQueryImpl<Unit, Unit>(query.Steps, query.StepLabelMappings);
        }

        internal static IGremlinQuery<TElement> Where<TElement>(this IGremlinQuery<TElement> query, ParameterExpression parameter, Expression left, Expression right, ExpressionType nodeType)
        {
            if (nodeType == ExpressionType.OrElse || nodeType == ExpressionType.AndAlso)
            {
                var leftLambda = Expression.Lambda<Func<TElement, bool>>(left, parameter);
                var rightLambda = Expression.Lambda<Func<TElement, bool>>(right, parameter);

                return nodeType == ExpressionType.OrElse
                    ? query
                        .Or(
                            _ => _.Where(leftLambda),
                            _ => _.Where(rightLambda))
                    : query
                        .And(
                            _ => _.Where(leftLambda),
                            _ => _.Where(rightLambda));
            }

            return query.Where(parameter, left, right.GetValue(), nodeType);
        }

        internal static IGremlinQuery<TElement> Where<TElement>(this IGremlinQuery<TElement> query, ParameterExpression parameter, Expression left, object rightConstant, ExpressionType nodeType)
        {
            if (rightConstant == null)
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (nodeType)
                {
                    case ExpressionType.Equal:
                        return query.Not(__ => __.Has(left));
                    case ExpressionType.NotEqual:
                        return query.Has(left);
                }
            }
            else
            {
                var predicateArgument = P.ForExpressionType(nodeType, rightConstant);

                switch (left)
                {
                    case MemberExpression leftMemberExpression when parameter == leftMemberExpression.Expression:
                    {
                        return query.Has(
                            leftMemberExpression,
                            rightConstant is StepLabel
                                ? query.ToAnonymous().AddStep("where", predicateArgument)
                                : (object)predicateArgument);
                    }
                    case ParameterExpression leftParameterExpression when parameter == leftParameterExpression:
                    {
                        return query.AddStep(
                            rightConstant is StepLabel
                                ? "where"
                                : "is",
                            predicateArgument);
                    }
                }
            }

            throw new NotSupportedException();
        }

        public static IGremlinQuery<TElement> Where<TElement>(this IGremlinQuery<TElement> query, Expression<Func<TElement, bool>> predicate)
        {
            var body = predicate.Body;

            switch (body)
            {
                case UnaryExpression unaryExpression:
                    {
                        if (unaryExpression.NodeType == ExpressionType.Not)
                            return query.Not(_ => _.Where(Expression.Lambda<Func<TElement, bool>>(unaryExpression.Operand, predicate.Parameters)));

                        break;
                    }
                case MemberExpression memberExpression:
                    {
                        if (memberExpression.Member is PropertyInfo property)
                        {
                            if (property.PropertyType == typeof(bool))
                                return query.Where(predicate.Parameters[0], memberExpression, Expression.Constant(true), ExpressionType.Equal);
                        }

                        break;
                    }
                case BinaryExpression binaryExpression:
                    return query.Where(predicate.Parameters[0], binaryExpression.Left.StripConvert(), binaryExpression.Right.StripConvert(), binaryExpression.NodeType);
                case MethodCallExpression methodCallExpression:
                    {
                        var methodInfo = methodCallExpression.Method;

                        if (methodInfo.DeclaringType == typeof(Enumerable))
                        {
                            // ReSharper disable once SwitchStatementMissingSomeCases
                            switch (methodInfo.Name)
                            {
                                case nameof(Enumerable.Contains) when methodInfo.GetParameters().Length == 2:
                                    {
                                        if (methodCallExpression.Arguments[0] is MemberExpression leftMember && leftMember.Expression == predicate.Parameters[0])
                                            return query.Has(leftMember, P.Eq(methodCallExpression.Arguments[1].GetValue()));

                                        if (methodCallExpression.Arguments[1] is MemberExpression rightMember && rightMember.Expression == predicate.Parameters[0])
                                        {
                                            if (methodCallExpression.Arguments[0].GetValue() is IEnumerable enumerable)
                                                return query.Has(rightMember, P.Within(enumerable.Cast<object>().ToArray()));
                                        }

                                        throw new NotSupportedException();
                                    }
                                case nameof(Enumerable.Any) when methodInfo.GetParameters().Length == 1:
                                    return query.Where(predicate.Parameters[0], methodCallExpression.Arguments[0], Expression.Constant(null, methodCallExpression.Arguments[0].Type), ExpressionType.NotEqual);
                            }
                        }
                        else if (methodInfo.DeclaringType == typeof(EnumerableExtensions))
                        {
                            if (methodInfo.Name == nameof(EnumerableExtensions.Intersects) && methodInfo.GetParameters().Length == 2)
                            {
                                if (methodCallExpression.Arguments[0] is MemberExpression innerMemberExpression)
                                {
                                    var constant = methodCallExpression.Arguments[1].GetValue();

                                    if (constant is IEnumerable arrayConstant)
                                        return query.Has(innerMemberExpression, P.Within(arrayConstant.Cast<object>().ToArray()));
                                }
                            }
                        }
                        else if (methodInfo.DeclaringType == typeof(string))
                        {
                            if (methodInfo.Name == nameof(string.StartsWith))
                            {
                                if (methodCallExpression.Arguments[0] is MemberExpression argumentExpression && argumentExpression.Expression == predicate.Parameters[0])
                                {
                                    if (methodCallExpression.Object.GetValue() is string stringValue)
                                    {
                                        return query.Has(
                                            argumentExpression,
                                            P.Within(Enumerable
                                                .Range(0, stringValue.Length + 1)
                                                .Select(i => stringValue.Substring(0, i))
                                                .Cast<object>()
                                                .ToArray()));
                                    }
                                }
                                else if (methodCallExpression.Object is MemberExpression memberExpression && memberExpression.Expression == predicate.Parameters[0])
                                {
                                    if (methodCallExpression.Arguments[0].GetValue() is string lowerBound)
                                    {
                                        string upperBound;

                                        if (lowerBound.Length == 0 || lowerBound[lowerBound.Length - 1] == char.MaxValue)
                                            upperBound = lowerBound + char.MinValue;
                                        else
                                        {
                                            var upperBoundChars = lowerBound.ToCharArray();
                                            
                                            upperBoundChars[upperBoundChars.Length - 1]++;
                                            upperBound = new string(upperBoundChars);
                                        }

                                        return query.Has(memberExpression, P.Between(lowerBound, upperBound));
                                    }
                                }
                            }
                        }

                        break;
                    }
            }

            throw new NotSupportedException();
        }

        internal static IGremlinQuery<TElement> Has<TElement>(this IGremlinQuery<TElement> query, Expression expression, Option<object> maybeArgument = default)
        {
            string name;

            switch (expression)
            {
                case MemberExpression leftMemberExpression:
                {
                    name = leftMemberExpression.Member.Name;
                    break;
                }
                case ParameterExpression leftParameterExpression:
                {
                    name = leftParameterExpression.Name;
                    break;
                }
                default:
                    throw new NotSupportedException();
            }

            return query.AddStep(new HasStep(name, maybeArgument));
        }

        public static IGremlinQuery<Unit> WithSubgraphStrategy(this IGremlinQuery<Unit> query, Func<IGremlinQuery<Unit>, IGremlinQuery> vertexCriterion, Func<IGremlinQuery<Unit>, IGremlinQuery> edgeCriterion)
        {
            var vertexCriterionTraversal = vertexCriterion(GremlinQuery.Anonymous);
            var edgeCriterionTraversal = edgeCriterion(GremlinQuery.Anonymous);

            if (vertexCriterionTraversal.Steps.Count > 1 || edgeCriterionTraversal.Steps.Count > 1)
            {
                var strategy = GremlinQuery
                    .Create("SubgraphStrategy")
                    .AddStep("build");

                if (vertexCriterionTraversal.Steps.Count > 0)
                    strategy = strategy.AddStep("vertices", vertexCriterionTraversal);

                if (edgeCriterionTraversal.Steps.Count > 0)
                    strategy = strategy.AddStep("edges", edgeCriterionTraversal);

                return query.AddStep(new MethodGremlinStep("withStrategies", strategy.AddStep("create")));
            }

            return query;
        }

        internal static Option<ITypedGremlinQueryProvider> TryGetTypedGremlinQueryProvider(this IGremlinQuery query)
        {
            return query
                .Steps
                .OfType<SetTypedGremlinQueryProviderGremlinStep>()
                .Select(x => Option<ITypedGremlinQueryProvider>.Some(x.TypedGremlinQueryProvider))
                .LastOrDefault();
        }

        internal static Option<IGraphModel> TryGetModel(this IGremlinQuery query)
        {
            return query
                .Steps
                .OfType<SetModelGremlinStep>()
                .Select(x => Option<IGraphModel>.Some(x.Model))
                .LastOrDefault();
        }
    }
}