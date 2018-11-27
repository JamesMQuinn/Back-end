using Funq;
using ServiceStack.Validation;
using ServiceStackServiceLog4NetTemplate.Validation;
using ServiceStackServiceLog4NetTemplate.Managers;
using ServiceStackServiceLog4NetTemplate.Repositories;
using ServiceStackServiceLog4NetTemplate.Interfaces.Managers;
using ServiceStackServiceLog4NetTemplate.Interfaces.Repositories;

namespace ServiceStackServiceLog4NetTemplate
{
    public static class ContainerManager
    {
        public static void Register(Container container)
        {
            // IReservationManager container
            container.RegisterAs<ReservationManager, IReservationManager>();
            // IReservationRepo container
            container.RegisterAs<SQLReservationRepository, IReservationRepo>();
            // add service validation
            container.RegisterValidators(ReuseScope.Container, typeof(ValidationInfo).Assembly);
        }

    }
}
