using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExRam.Gremlinq
{
    public sealed class ReplaceElementPropertyStep<TSource, TValue> : DecorateAddElementPropertiesStep<TSource, TValue>
    {
        public ReplaceElementPropertyStep(AddElementPropertiesStep baseStep, Expression<Func<TSource, TValue>> memberExpression, TValue value) : base(baseStep, memberExpression, value)
        {
        }

        public override IEnumerable<TerminalGremlinStep> Resolve(IGraphModel model)
        {
            foreach (var step in this.BaseStep.Resolve(model))
            {
                if (step is MethodGremlinStep methodStep)
                {
                    if (methodStep.Name == "property" && methodStep.Parameters.Count > 0 && methodStep.Parameters[0] as string == this.Key)
                        yield return new MethodGremlinStep("property", this.Key, this.Value);
                    else
                        yield return step;
                }
            }
        }
    }
}