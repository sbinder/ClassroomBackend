using ClassroomBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ClassroomBackend.Controllers
{
    public class ClassroomController : ApiController
    {

        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);

        private string safeString(MySqlDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
            {
                return string.Empty;
            }
            else
            {
                return reader.GetString(field);
            }
        }
        private int safeInt(MySqlDataReader reader, string field)
        {
            if (reader.IsDBNull(reader.GetOrdinal(field)))
            {
                return 0;
            }
            else
            {
                return reader.GetInt32(field);
            }
        }

        [HttpGet]
        public HttpResponseMessage Students()
        {
            MySqlCommand cmd = db.CreateCommand();
            cmd.CommandText = "select stid, target, lname, fname, liturgy from student where org = 1";
            List<Student> students = new List<Student>();
            try
            {
                db.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var s = new Student
                    {
                        stid = reader.GetUInt32("stid"),
                        fname = safeString(reader, "fname"), //reader.GetString("fname"),
                        lname = safeString(reader, "lname"),    // reader.GetString("lname"),
                        target = reader.GetDateTime("target"),
                        group = safeInt(reader, "liturgy")  //reader.GetInt16("liturgy")
                    };
                    students.Add(s);
                }
                IEnumerable<Student> responseBody = students;
                return Request.CreateResponse(HttpStatusCode.OK, responseBody);

            }
            catch (Exception r)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                // DO NOT DO THIS IN PRODUCTION!
                var replacement = r.ToString().Replace('\n', '*').Replace('\r', '*').Substring(0,255); //Regex.Replace(r.ToString(), @"\t|\n|\r", "*");
                response.ReasonPhrase = replacement;
                return response;
            }
        }


        [HttpPost]
        //public IEnumerable<Progress> Prayers(List<uint> students)
        public HttpResponseMessage Prayers(List<uint> students)
        {
            List<Progress> prayers = new List<Progress>();
            var slist = String.Join(",", students);
            MySqlCommand cmd = db.CreateCommand();

            cmd.CommandText = @"SELECT o.* FROM `progress` o LEFT JOIN `progress` b " +
                " ON o.stid = b.stid AND o.taskid = b.taskid AND o.date < b.date " +
                "WHERE o.stid in (" + slist + ") and b.date is NULL";

            try
            {
                db.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Progress
                    {
                        stid = reader.GetUInt32("stid"),
                        taskid = reader.GetUInt32("taskid"),
                        changed = reader.GetDateTime("date"),
                        rating = safeInt(reader, "rating"),
                        scomment = safeString(reader, "scomment"),
                        tcomment = safeString(reader, "tcomment"),
                        assigned = true
                    };
                    prayers.Add(p);
                }
                IEnumerable<Progress> responseBody = prayers;
                return Request.CreateResponse(HttpStatusCode.OK, responseBody);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                // DO NOT DO THIS IN PRODUCTION!
                var replacement = ex.ToString().Replace('\n', '*').Replace('\r', '*').Substring(0, 255); 
                response.ReasonPhrase = replacement;
                return response;
            }
        }
    }
}