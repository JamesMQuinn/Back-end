using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStackServiceLog4NetTemplate.Interfaces.Managers;
using ServiceStackServiceLog4NetTemplate.Interfaces.Repositories;
using ServiceStackServiceLog4NetTemplate.Repositories;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Types;

namespace ServiceStackServiceLog4NetTemplate.Managers
{
    public class ReservationManager : IReservationManager
    {
        private IReservationRepo _reservationRepo;

        public ReservationManager(IReservationRepo reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }

        public List<Reservation> GetAllReservations()
        {
            var theReservations = _reservationRepo.RetrieveAllReservations();
            return theReservations;
        }

        public void AddAllReservations(string Name, string Cabin, DateTime CheckIn, DateTime CheckOut)
        {
            _reservationRepo.AddReservation(Name, Cabin, CheckIn, CheckOut);
        }
    }
}
