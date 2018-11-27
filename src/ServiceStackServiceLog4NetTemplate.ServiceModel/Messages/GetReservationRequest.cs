using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace ServiceStackServiceLog4NetTemplate.ServiceModel.Messages
{
    /// <summary>
    /// Get route for the reservations that exist within the DB.
    /// </summary>
    [Route("/GetReservations", Verbs = "GET")]
    public class GetReservationRequest : IReturn<GetReservationResponse>
    {
    }
}
