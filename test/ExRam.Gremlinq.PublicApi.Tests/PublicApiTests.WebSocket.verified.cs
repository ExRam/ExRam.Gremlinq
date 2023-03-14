﻿namespace ExRam.Gremlinq.Core
{
    public static class ConfigurableGremlinQuerySourceExtensions
    {
        public static ExRam.Gremlinq.Core.IGremlinQuerySource UseWebSocket(this ExRam.Gremlinq.Core.IConfigurableGremlinQuerySource source, System.Func<ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator, ExRam.Gremlinq.Core.IGremlinQuerySourceTransformation> configuratorTransformation) { }
    }
    public static class GremlinClientExtensions
    {
        public static Gremlin.Net.Driver.IGremlinClient ObserveResultStatusAttributes(this Gremlin.Net.Driver.IGremlinClient client, System.Action<Gremlin.Net.Driver.Messages.RequestMessage, System.Collections.Generic.IReadOnlyDictionary<string, object>> observer) { }
        public static Gremlin.Net.Driver.IGremlinClient TransformRequest(this Gremlin.Net.Driver.IGremlinClient client, System.Func<Gremlin.Net.Driver.Messages.RequestMessage, System.Threading.Tasks.Task<Gremlin.Net.Driver.Messages.RequestMessage>> transformation) { }
    }
}
namespace ExRam.Gremlinq.Providers.WebSocket
{
    public static class GremlinClientFactory
    {
        public static readonly ExRam.Gremlinq.Providers.WebSocket.IGremlinClientFactory Default;
        public static ExRam.Gremlinq.Providers.WebSocket.IGremlinClientFactory Create(System.Func<Gremlin.Net.Driver.GremlinServer, Gremlin.Net.Driver.IMessageSerializer, Gremlin.Net.Driver.ConnectionPoolSettings, System.Action<System.Net.WebSockets.ClientWebSocketOptions>, string?, Gremlin.Net.Driver.IGremlinClient> factory) { }
    }
    public static class GremlinServerExtensions
    {
        public static Gremlin.Net.Driver.GremlinServer WithHost(this Gremlin.Net.Driver.GremlinServer server, string host) { }
        public static Gremlin.Net.Driver.GremlinServer WithPassword(this Gremlin.Net.Driver.GremlinServer server, string password) { }
        public static Gremlin.Net.Driver.GremlinServer WithPort(this Gremlin.Net.Driver.GremlinServer server, int port) { }
        public static Gremlin.Net.Driver.GremlinServer WithSslEnabled(this Gremlin.Net.Driver.GremlinServer server, bool sslEnabled) { }
        public static Gremlin.Net.Driver.GremlinServer WithUsername(this Gremlin.Net.Driver.GremlinServer server, string username) { }
    }
    public interface IGremlinClientFactory
    {
        Gremlin.Net.Driver.IGremlinClient Create(Gremlin.Net.Driver.GremlinServer gremlinServer, Gremlin.Net.Driver.IMessageSerializer messageSerializer, Gremlin.Net.Driver.ConnectionPoolSettings connectionPoolSettings, System.Action<System.Net.WebSockets.ClientWebSocketOptions> webSocketConfiguration, string? sessionId = null);
    }
    public interface IWebSocketConfigurator : ExRam.Gremlinq.Core.IGremlinQuerySourceTransformation
    {
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator ConfigureAlias(System.Func<string, string> transformation);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator ConfigureClientFactory(System.Func<ExRam.Gremlinq.Providers.WebSocket.IGremlinClientFactory, ExRam.Gremlinq.Providers.WebSocket.IGremlinClientFactory> transformation);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator ConfigureServer(System.Func<Gremlin.Net.Driver.GremlinServer, Gremlin.Net.Driver.GremlinServer> transformation);
    }
    public interface IWebSocketProviderConfigurator<out TConfigurator> : ExRam.Gremlinq.Core.IGremlinQuerySourceTransformation, ExRam.Gremlinq.Core.IGremlinqConfigurator<TConfigurator>, ExRam.Gremlinq.Providers.Core.IProviderConfigurator<TConfigurator>
        where out TConfigurator : ExRam.Gremlinq.Providers.Core.IProviderConfigurator<TConfigurator>
    {
        TConfigurator ConfigureWebSocket(System.Func<ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator, ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator> transformation);
    }
    public static class WebSocketConfiguratorExtensions
    {
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator At(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator builder, string uri) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator At(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator configurator, System.Uri uri) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator AtLocalhost(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator builder) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator AuthenticateBy(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator configurator, string username, string password) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator ConfigureClient(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator configurator, System.Func<Gremlin.Net.Driver.IGremlinClient, Gremlin.Net.Driver.IGremlinClient> transformation) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator ConfigureMessageSerializer(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator configurator, System.Func<Gremlin.Net.Driver.IMessageSerializer, Gremlin.Net.Driver.IMessageSerializer> transformation) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator SetAlias(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator configurator, string alias) { }
    }
    public static class WebSocketProviderConfiguratorExtensions
    {
        public static TConfigurator At<TConfigurator>(this TConfigurator configurator, System.Uri uri)
            where TConfigurator : ExRam.Gremlinq.Providers.WebSocket.IWebSocketProviderConfigurator<TConfigurator> { }
        public static TConfigurator AtLocalhost<TConfigurator>(this TConfigurator configurator)
            where TConfigurator : ExRam.Gremlinq.Providers.WebSocket.IWebSocketProviderConfigurator<TConfigurator> { }
    }
}