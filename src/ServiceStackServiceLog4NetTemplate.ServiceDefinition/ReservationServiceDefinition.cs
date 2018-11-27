using ServiceStack;
using ServiceStackServiceLog4NetTemplate.Interfaces.Managers;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Messages;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackServiceLog4NetTemplate.ServiceDefinition
{
    public class ReservationServiceDefinition : Service
    {
        private IReservationManager _reservationManager;

        public ReservationServiceDefinition(IReservationManager reservationM)
        {
            _reservationManager = reservationM;
        }

        public GetReservationResponse Get(GetReservationRequest request)
        {
            var allReservations = _reservationManager.GetAllReservations();

            GetReservationResponse resp = new GetReservationResponse()
            {
                Reservations = allReservations
            };
            return resp;
        }

        public void Post(AddReservationRequest request)
        {
            _reservationManager.AddAllReservations(request.Name, request.Cabin, request.CheckIn, request.CheckOut);
        }
    }
}
