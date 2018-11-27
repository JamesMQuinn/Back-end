using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Types;
using ServiceStack;

namespace ServiceStackServiceLog4NetTemplate.ServiceModel.Messages
{
    /// <summary>
    /// Response from the GetReservationRequest
    /// </summary>
    public class GetReservationResponse : IHasResponseStatus
    {
        /// <summary>
        /// Get the listed reservations
        /// </summary>
        public List<Reservation> Reservations { get; set; }
        /// <summary>
        /// Get the error response status, if there is one.
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }
    }
}
