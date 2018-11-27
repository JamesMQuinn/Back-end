using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStackServiceLog4NetTemplate.Interfaces.Managers;
using ServiceStackServiceLog4NetTemplate.Interfaces.Repositories;
using ServiceStackServiceLog4NetTemplate.ServiceModel.Types;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace ServiceStackServiceLog4NetTemplate.Repositories
{
    public class SQLReservationRepository : IReservationRepo
    {
        private readonly string connectionString = ConfigurationManager.AppSettings["ReservationsDB"];

        public List<Reservation> RetrieveAllReservations()
        {
            var query = @"SELECT * FROM Reservations";
            var allReservations = GetReservations(query);
            return allReservations;
        }

        private List<Reservation> GetReservations(string query, params SqlParameter[] sqlParameters)
        {
            var listOfReservations = new List<Reservation>();
            SqlConnection reservationsDBConnection = new SqlConnection(connectionString);
            SqlCommand reservationsQuery = new SqlCommand(query, reservationsDBConnection);

            reservationsQuery.Parameters.AddRange(sqlParameters);

            using (reservationsDBConnection)
            {
                reservationsDBConnection.Open();
                var resultSet = reservationsQuery.ExecuteReader();

                while (resultSet.Read())
                {
                    var newReservation = new Reservation()
                    {
                        Name = resultSet["name"].ToString(),
                        Cabin = resultSet["cabin"].ToString(),
                        CheckIn = (DateTime)resultSet["CheckIn"],
                        CheckOut = (DateTime)resultSet["CheckOut"]
                    };
                    listOfReservations.Add(newReservation);
                }
                reservationsDBConnection.Close();
            }
            return listOfReservations;
        }

        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request["param1"];
            string cabin = context.Request["param2"];
            DateTime CheckIn = DateTime.Parse(context.Request["param3"]);
            DateTime CheckOut = DateTime.Parse(context.Request["param4"]);
        }

        public void AddReservation(string name, string cabin, DateTime CheckIn, DateTime CheckOut)
        {
            Console.WriteLine("param1");

            var query = @"INSERT INTO reservations ( name, cabin, CheckIn, CheckOut ) VALUES (@name, @cabin, @CheckIn, @CheckOut)";
            SqlConnection reservationsDBConnection = new SqlConnection(connectionString);
            SqlCommand reservationsQuery = new SqlCommand(query, reservationsDBConnection);

            reservationsQuery = reservationsDBConnection.CreateCommand();
            reservationsQuery.CommandText = @"INSERT INTO reservations(name, cabin, CheckIn, CheckOut ) VALUES (@name, @cabin, @CheckIn, @CheckOut)";
            reservationsQuery.Parameters.AddWithValue("@name", name);
            reservationsQuery.Parameters.AddWithValue("@cabin", cabin);
            reservationsQuery.Parameters.AddWithValue("@CheckIn", CheckIn);
            reservationsQuery.Parameters.AddWithValue("@CheckOut", CheckOut);

            reservationsDBConnection.Open();
            reservationsQuery.ExecuteNonQuery();
            reservationsDBConnection.Close();
        }

    }
}
