﻿using ExRam.Gremlinq.Core.Serialization;

namespace ExRam.Gremlinq.Core
{
    public abstract class TextP : P
    {
        public sealed class StartingWith : TextP
        {
            public StartingWith(string prefix)
            {
                Prefix = prefix;
            }

            public override void Accept(IGremlinQueryElementVisitor visitor)
            {
                visitor.Visit(this);
            }

            internal override bool ContainsSingleStepLabel()
            {
                return false;
            }

            internal override P WorkaroundLimitations(Options options)
            {
                if ((options.DisabledTextPredicates & DisabledTextPredicates.StartingWith) == 0)
                    return base.WorkaroundLimitations(options);

                var upperBound = Prefix;

                if (Prefix[Prefix.Length - 1] == char.MaxValue)
                    upperBound = Prefix + char.MinValue;
                else
                {
                    var upperBoundChars = Prefix.ToCharArray();

                    upperBoundChars[upperBoundChars.Length - 1]++;
                    upperBound = new string(upperBoundChars);
                }

                return new P.Between(Prefix, upperBound);
            }

            public string Prefix { get; }
        }
    }
}
