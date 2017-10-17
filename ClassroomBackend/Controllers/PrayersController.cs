using ClassroomBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClassroomBackend.Controllers
{
    public class PrayersController : ApiController
    {

        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);

        [HttpGet]
        //public IEnumerable<Prayer> Prayers()
        public HttpResponseMessage Prayers()
        {

            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);


            MySqlCommand cmd = db.CreateCommand();
            cmd.CommandText = "select taskid, taskname, ordinal, groupa, groupb, groupx from task where org = 1 and active = 1 order by ordinal";
            List<Prayer> prayers = new List<Prayer>();
            try
            {
                db.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Prayer
                    {
                        taskid = reader.GetUInt32("taskid"),
                        ordinal = reader.GetInt32("ordinal"),
                        groupa = reader.GetBoolean("groupa"),
                        groupb = reader.GetBoolean("groupb"),
                        groupx = reader.GetBoolean("groupx"),
                        description = reader.GetString("taskname")
                    };
                    prayers.Add(p);
                }
                //IEnumerable<Prayer> responseBody = prayers;
                //return Request.CreateResponse(HttpStatusCode.OK, responseBody);

                //return prayers;

            }
            catch (Exception r)
            {
                Console.WriteLine("Error: " + r);
                prayers = new List<Prayer>();   // make sure it's empty
            }
            IEnumerable<Prayer> responseBody = prayers;
            return Request.CreateResponse(HttpStatusCode.OK, responseBody);

            //return new List<Prayer>();
        }

    }
}
