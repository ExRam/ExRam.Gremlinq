﻿namespace ExRam.Gremlinq.Core.Steps
{
    public sealed class AddVStep : Step
    {
        public AddVStep(string label) : base(SideEffectSemanticsChange.Write)
        {
            Label = label;
        }

        public string Label { get; }
    }
}
