namespace ExRam.Gremlinq.Core
{
    public static class ConfigurableGremlinQuerySourceExtensions
    {
        public static ExRam.Gremlinq.Core.IGremlinQuerySource UseGremlinServer(this ExRam.Gremlinq.Core.IConfigurableGremlinQuerySource source, System.Func<ExRam.Gremlinq.Providers.GremlinServer.IGremlinServerConfigurator, ExRam.Gremlinq.Core.IGremlinQuerySourceTransformation> configuratorTransformation) { }
    }
}
namespace ExRam.Gremlinq.Providers.GremlinServer
{
    public static class GremlinServerConfiguratorExtensions
    {
        public static ExRam.Gremlinq.Providers.GremlinServer.IGremlinServerConfigurator AtLocalhost(this ExRam.Gremlinq.Providers.GremlinServer.IGremlinServerConfigurator configurator) { }
    }
    public static class GremlinServerGremlinqOptions
    {
        public static readonly ExRam.Gremlinq.Core.GremlinqOption<bool> WorkaroundTinkerpop2112;
    }
    public interface IGremlinServerConfigurator : ExRam.Gremlinq.Core.IGremlinQuerySourceTransformation, ExRam.Gremlinq.Providers.WebSocket.IProviderConfigurator<ExRam.Gremlinq.Providers.GremlinServer.IGremlinServerConfigurator> { }
}