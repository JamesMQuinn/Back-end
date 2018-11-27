using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackServiceLog4NetTemplate.ServiceModel.Types
{
    /// <summary>
    /// Class creation for Reservation for use in the DB table.
    /// </summary>
    public class Reservation
    {
        public string Name { get; set; }
        public string Cabin { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
