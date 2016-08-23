using Funq;
using ServiceStack.Validation;
using ServiceStackServiceLog4NetTemplate.Validation;

namespace ServiceStackServiceLog4NetTemplate
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