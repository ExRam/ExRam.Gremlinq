﻿using System.Linq.Expressions;

using ExRam.Gremlinq.Providers.Core;

namespace ExRam.Gremlinq.Providers.CosmosDb
{
    public interface ICosmosDbConfigurator<TVertexBase> : IProviderConfigurator<ICosmosDbConfigurator<TVertexBase>, IPoolGremlinqClientFactory<IWebSocketGremlinqClientFactory>>
    {
        ICosmosDbConfigurator<TVertexBase> WithPartitionKey(Expression<Func<TVertexBase, object>> partitionKeyExpression);

        ICosmosDbConfigurator<TVertexBase> OnDatabase(string databaseName);

        ICosmosDbConfigurator<TVertexBase> OnGraph(string graphName);

        ICosmosDbConfigurator<TVertexBase> AuthenticateBy(string authKey);
    }
}
