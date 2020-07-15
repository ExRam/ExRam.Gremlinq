[assembly: System.Runtime.Versioning.TargetFramework(".NETStandard,Version=v2.1", FrameworkDisplayName="")]
namespace ExRam.Gremlinq.Core
{
    public static class GremlinQueryEnvironmentExtensions
    {
        public static ExRam.Gremlinq.Core.IGremlinQueryEnvironment UseWebSocket(this ExRam.Gremlinq.Core.IGremlinQueryEnvironment environment, System.Func<ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder, ExRam.Gremlinq.Core.IGremlinQueryExecutorBuilder> builderTransformation) { }
    }
}
namespace ExRam.Gremlinq.Providers.WebSocket
{
    public interface IWebSocketGremlinQueryExecutorBuilder : ExRam.Gremlinq.Core.IGremlinQueryExecutorBuilder
    {
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder AddGraphSONDeserializer(string typename, Gremlin.Net.Structure.IO.GraphSON.IGraphSONDeserializer serializer);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder AddGraphSONSerializer(System.Type type, Gremlin.Net.Structure.IO.GraphSON.IGraphSONSerializer serializer);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder At(System.Uri uri);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder AuthenticateBy(string username, string password);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder ConfigureConnectionPool(System.Action<Gremlin.Net.Driver.ConnectionPoolSettings> transformation);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder ConfigureGremlinClient(System.Func<Gremlin.Net.Driver.IGremlinClient, Gremlin.Net.Driver.IGremlinClient> transformation);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder SetAlias(string alias);
        ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder SetSerializationFormat(ExRam.Gremlinq.Core.SerializationFormat version);
    }
    public static class WebSocketGremlinQueryEnvironmentBuilderExtensions
    {
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder At(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder builder, string uri) { }
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder AtLocalhost(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketGremlinQueryExecutorBuilder builder) { }
    }
}