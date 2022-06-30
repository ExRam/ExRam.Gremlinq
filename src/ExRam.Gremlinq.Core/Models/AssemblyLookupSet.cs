﻿using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;

namespace ExRam.Gremlinq.Core.Models
{
    internal sealed class AssemblyLookupSet : IAssemblyLookupSet
    {
        public static readonly IAssemblyLookupSet Empty = new AssemblyLookupSet(Array.Empty<Type>(), ImmutableHashSet<Assembly>.Empty);

        private readonly Type[] _baseTypes;

        public AssemblyLookupSet(Type[] baseTypes, IImmutableSet<Assembly> assemblies)
        {
            _baseTypes = baseTypes;
            Assemblies = assemblies;
        }

        public IAssemblyLookupSet IncludeAssembliesOfBaseTypes()
        {
            return IncludeAssemblies(_baseTypes.Select(static x => x.Assembly));
        }

        public IAssemblyLookupSet IncludeAssembliesFromStackTrace()
        {
            return IncludeAssemblies(new StackTrace().GetFrames()
                .Select(static frame => frame.GetMethod()?.DeclaringType?.Assembly)
                .Where(static assembly => assembly != null)
                .Select(static assembly => assembly!));
        }

        public IAssemblyLookupSet IncludeAssembliesFromAppDomain()
        {
            return IncludeAssemblies(AppDomain.CurrentDomain.GetAssemblies());
        }

        public IAssemblyLookupSet IncludeAssemblies(IEnumerable<Assembly> assemblies)
        {
            return new AssemblyLookupSet(
                _baseTypes,
                Assemblies
                    .AddRange(assemblies));
        }

        public IImmutableSet<Assembly> Assemblies { get; }
    }
}
