﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExRam.Gremlinq
{
    internal interface IJsonTransform
    {
        IEnumerator<(JsonToken tokenType, object tokenValue)> Transform(IPebbleEnumerator<(JsonToken tokenType, object tokenValue)> enumerator, IJsonTransform recurse);
    }
}
