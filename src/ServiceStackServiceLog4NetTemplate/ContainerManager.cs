using Funq;
using ServiceStack.Validation;
using ServiceStackServiceTemplate.Validation;

namespace ServiceStackServiceTemplate
{
    public static class ContainerManager
    {
        public static void Register(Container container)
        {
            // add service validation
            container.RegisterValidators(ReuseScope.Container, typeof(ValidationInfo).Assembly);
        }

    }
}