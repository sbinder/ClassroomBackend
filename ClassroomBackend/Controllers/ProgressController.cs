using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClassroomBackend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class ProgressController : ApiController
    {
        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);

        [HttpPost]
        public HttpResponseMessage Progress(List<int> students)

        {
            return new HttpResponseMessage(HttpStatusCode.OK);
/*
            MySqlCommand cmd = db.CreateCommand();
            cmd.CommandText = "select taskid, taskname, ordinal, groupa, groupb, groupx from task where org = 1 and active = 1";
            List<Prayer> prayers = new List<Prayer>();
            try
            {
                db.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Prayer
                    {
                        prid = reader.GetUInt32("TaskID"),
                        ordinal = reader.GetInt32("ordinal"),
                        groupa = reader.GetBoolean("groupa"),
                        groupb = reader.GetBoolean("groupb"),
                        groupx = reader.GetBoolean("groupx"),
                        description = reader.GetString("taskname")
                    };
                    prayers.Add(p);
                }

                return prayers;

            }
            catch (Exception r)
            {
                Console.WriteLine("Error: " + r);
            }

            return new List<Prayer>();
        }
*/
        }

    }
}
