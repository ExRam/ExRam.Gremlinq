﻿#pragma warning disable IDE0003
// ReSharper disable ArrangeThisQualifier

namespace ExRam.Gremlinq.Core
{
    public interface IRepeatUntilLoopBuilder<out TQuery> : IFinalLoopBuilder<TQuery>
        where TQuery : IGremlinQueryBase
    {

    }
}
