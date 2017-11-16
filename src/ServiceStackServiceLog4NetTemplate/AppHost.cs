using CHR.ServiceStack.Plugins;
using Funq;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Text;
using ServiceStack.Validation;
using ServiceStackServiceLog4NetTemplate.ServiceDefinition;
using ServiceStackServiceLog4NetTemplate.Validation;

namespace ServiceStackServiceLog4NetTemplate
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost() : base(ServiceDefinitionInfo.Name, ServiceDefinitionInfo.Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            JsConfig.EmitCamelCaseNames = true;
            JsConfig.DateHandler = DateHandler.ISO8601;

            ContainerManager.Register(container);

            InitializePlugins(container);

            //capture validation and service errors 
            this.ServiceExceptionHandlers.Add((httpReq, request, exception) =>
            {
                //log exception
                EnterpriseMonitoring.Logging.NetFramework.LogManager.Logger.TrackException(exception,EnterpriseMonitoring.Logging.NetFramework.LogLevel.Verbose, "ServiceExceptionHandlers");

                //continue with default Error Handling
                return null;
            });
        }

        private void InitializePlugins(Container container)
        {
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new SwaggerFeature());
            Plugins.Add(new HealthCheckFeature(container));
            Plugins.Add(new CorrelationIdFeature());
        }
    }
}
