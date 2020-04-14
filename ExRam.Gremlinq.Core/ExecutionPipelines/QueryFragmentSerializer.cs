﻿using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Threading;

namespace ExRam.Gremlinq.Core
{
    public interface IQueryFragmentSerializer
    {
        object Serialize<TFragment>(TFragment fragment);

        IQueryFragmentSerializer Override<TFragment>(QueryFragmentSerializer<TFragment> serializer);
    }

    public static class QueryFragmentSerializer
    {
        private sealed class FragmentSerializerImpl : IQueryFragmentSerializer
        {
            private readonly IImmutableDictionary<Type, Delegate> _dict;
            private ConcurrentDictionary<(Type staticType, Type actualType), Delegate?>? _fastDict;

            public FragmentSerializerImpl(IImmutableDictionary<Type, Delegate> dict)
            {
                _dict = dict;
            }

            public object Serialize<TFragment>(TFragment fragment)
            {
                return TryGetSerializer(typeof(TFragment), fragment.GetType()) is Func<TFragment, object> del
                    ? del(fragment)
                    : fragment;
            }

            public IQueryFragmentSerializer Override<TFragment>(QueryFragmentSerializer<TFragment> serializer)
            {
                return new FragmentSerializerImpl(
                    _dict.SetItem(
                        typeof(TFragment),
                        _dict.TryGetValue(typeof(TFragment), out var existingAtomSerializer)
                            ? new QueryFragmentSerializer<TFragment>((atom, baseSerializer, recurse) => serializer(atom, _ => ((QueryFragmentSerializer<TFragment>)existingAtomSerializer)(_!, baseSerializer, recurse), recurse))
                            : (atom, baseSerializer, recurse) => serializer(atom, _ => baseSerializer(_!), recurse)));
            }

            private Delegate? TryGetSerializer(Type staticType, Type actualType)
            {
                var fastDict = Volatile.Read(ref _fastDict);
                if (fastDict == null)
                    Volatile.Write(ref _fastDict, fastDict = new ConcurrentDictionary<(Type, Type), Delegate?>());

                return fastDict
                    .GetOrAdd(
                        (staticType, actualType),
                        (typeTuple, @this) =>
                        {
                            Delegate? InnerLookup(Type actualType)
                            {
                                if (@this._dict.TryGetValue(actualType, out var ret))
                                    return ret;

                                foreach (var implementedInterface in actualType.GetInterfaces())
                                {
                                    if (InnerLookup(implementedInterface) is { } interfaceSerializer)
                                        return interfaceSerializer;
                                }

                                if (actualType.BaseType is { } baseType)
                                {
                                    if (InnerLookup(baseType) is { } baseSerializer)
                                        return baseSerializer;
                                }

                                return null;
                            }

                            if (InnerLookup(typeTuple.actualType) is { } del)
                            {
                                //return (TStatic fragment) => del((TActualType)fragment, (TActual _) => _, @this);

                                var effectiveType = del.GetType().GetGenericArguments()[0];

                                var argument2Parameter = Expression.Parameter(effectiveType);
                                var fragmentParameterExpression = Expression.Parameter(staticType);

                                var retCall = Expression.Invoke(
                                    Expression.Constant(del),
                                    Expression.Convert(
                                        fragmentParameterExpression,
                                        effectiveType),
                                    Expression.Lambda(
                                        typeof(Func<,>).MakeGenericType(effectiveType, typeof(object)), //Func<TActual, object>
                                        Expression.Convert(argument2Parameter, typeof(object)),
                                        argument2Parameter),
                                    Expression.Constant(@this));

                                return Expression
                                    .Lambda(
                                        typeof(Func<,>)
                                            .MakeGenericType(staticType, typeof(object)),
                                        retCall,
                                        fragmentParameterExpression)
                                    .Compile();
                            }

                            return null;
                        },
                        this);
            }
        }

        public static readonly IQueryFragmentSerializer Identity = new FragmentSerializerImpl(ImmutableDictionary<Type, Delegate>.Empty);
    }

    public delegate object QueryFragmentSerializer<TFragment>(TFragment fragment, Func<TFragment, object> baseSerializer, IQueryFragmentSerializer recurse);
}
