namespace ExRam.Gremlinq.Core.AspNet
{
    public static class GremlinqSetupExtensions
    {
        public static ExRam.Gremlinq.Core.AspNet.GremlinqSetup UseProvider<TConfigurator>(this ExRam.Gremlinq.Core.AspNet.GremlinqSetup setup, string sectionName, System.Func<ExRam.Gremlinq.Core.IConfigurableGremlinQuerySource, System.Func<TConfigurator, ExRam.Gremlinq.Core.IGremlinQuerySourceTransformation>, ExRam.Gremlinq.Core.IGremlinQuerySource> providerChoice, System.Func<TConfigurator, Microsoft.Extensions.Configuration.IConfiguration, TConfigurator> configuration, System.Func<TConfigurator, Microsoft.Extensions.Configuration.IConfiguration, TConfigurator>? extraConfiguration = null)
            where TConfigurator : ExRam.Gremlinq.Providers.Core.IProviderConfigurator<TConfigurator> { }
    }
    public static class WebSocketConfiguratorExtensions
    {
        public static ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator ConfigureFrom(this ExRam.Gremlinq.Providers.WebSocket.IWebSocketConfigurator webSocketConfigurator, Microsoft.Extensions.Configuration.IConfiguration configuration) { }
    }
}