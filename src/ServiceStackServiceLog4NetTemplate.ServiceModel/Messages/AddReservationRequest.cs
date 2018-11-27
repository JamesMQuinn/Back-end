using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace ServiceStackServiceLog4NetTemplate.ServiceModel.Messages
{
    /// <summary>
    /// Post route for a reservation to be set in the DB with the expected return class.
    /// </summary>
    [Route("/AddReservation", Verbs = "POST")]
    public class AddReservationRequest : IReturn<AddReservationResponse>
    {
        public string Name { get; private set; }
        public string Cabin { get; private set; }
        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
    }
}
