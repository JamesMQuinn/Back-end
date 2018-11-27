using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Types;

namespace ServiceStackServiceLog4NetTemplate.Interfaces.Managers
{
    public interface IReservationManager
    {
        List<Reservation> GetAllReservations();
        void AddAllReservations(string Name, string Cabin, DateTime CheckIn, DateTime CheckOut);
    }
}
