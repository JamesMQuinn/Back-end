using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStackServiceLog4NetTemplate.Interfaces.Managers;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Types;

namespace ServiceStackServiceLog4NetTemplate.Interfaces.Repositories
{
    public interface IReservationRepo
    {
        List<Reservation> RetrieveAllReservations();
        void AddReservation(string Name, string Cabin, DateTime CheckIn, DateTime CheckOut);
    }
}
