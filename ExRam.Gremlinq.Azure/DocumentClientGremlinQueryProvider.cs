﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Xml;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Graphs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace ExRam.Gremlinq.Azure
{
    public sealed class DocumentClientGremlinQueryProvider : INativeGremlinQueryProvider
    {
        private readonly ILogger _logger;
        private readonly DocumentClient _client;
        private readonly DocumentCollection _graph;

        public DocumentClientGremlinQueryProvider(IOptions<CosmosDbGraphConfiguration> configuration, ILogger logger)
        {
            this._logger = logger;
            this._client =  new DocumentClient(
                new Uri(configuration.Value.EndPoint),
                configuration.Value.AuthKey,
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp });

            this._graph = this._client.CreateDocumentCollectionIfNotExistsAsync(
                UriFactory.CreateDatabaseUri(configuration.Value.Database),
                new DocumentCollection { Id = configuration.Value.GraphName },
                new RequestOptions { OfferThroughput = 1000 }).Result;  //TODO: Async!

            this.TraversalSource = GremlinQuery.Create(configuration.Value.TraversalSource);
        }

        public IAsyncEnumerable<string> Execute(string query, IDictionary<string, object> parameters)
        {
            foreach (var kvp in parameters.OrderByDescending(x => x.Key.Length))
            {
                var value = kvp.Value;

                switch (value)
                {
                    case Enum _:
                        value = (int)value;
                        break;
                    case DateTimeOffset x:
                        value = x.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ");
                        break;
                    case DateTime x:
                        value = x.ToUniversalTime().ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffZ");
                        break;
                    case TimeSpan x:
                        value = XmlConvert.ToString(x);
                        break;
                    case byte[] x:
                        value = Convert.ToBase64String(x);
                        break;
                }

                if (value is string)
                    value = $"'{value}'";
                else
                    value = value.ToString().ToLower();

                query = query.Replace(kvp.Key, (string)value);
            }

            this._logger.LogTrace("Executing Gremlin query {0}.", query);

            var documentQuery = this._client.CreateGremlinQuery<JToken>(this._graph, query);

            return AsyncEnumerable
                .Repeat(Unit.Default)
                .TakeWhile(_ => documentQuery.HasMoreResults)
                .SelectMany(async (_, ct) =>
                {
                    try
                    {
                        return await documentQuery.ExecuteNextAsync<JToken>(ct).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(query, ex);
                    }
                })
                .SelectMany(x => x.ToAsyncEnumerable())
                .Select(x => x.ToString());
        }

        public IGremlinQuery<Unit> TraversalSource { get; }
    }
}
