using Funq;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Text;
using ServiceStack.Validation;
using ServiceStackServiceTemplate.ServiceDefinition;
using ServiceStackServiceTemplate.Validation;

namespace ServiceStackServiceTemplate
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

            InitializePlugins();
            InitializeContainer(container);
        }

        private void InitializePlugins()
        {
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new SwaggerFeature());
        }

        private void InitializeContainer(Container container)
        {
            container.RegisterValidators(ReuseScope.Container, typeof(ValidationInfo).Assembly);
        }
    }
}
