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
    public class StudentController : ApiController
    {

        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);

        [HttpGet]
        public HttpResponseMessage Get(uint id = 0)
        {
            var stid = id;
            MySqlCommand cmd = db.CreateCommand();
            if (stid == 0)
            {
                cmd.CommandText = "select stid, target, lname, fname, liturgy from student where org = 1";
            }
            else
            {
                cmd.CommandText = "select stid, target, lname, fname, liturgy from student where stid = @stid";
                cmd.Parameters.AddWithValue("@stid", stid);
            }
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
                        fname = SqlHelper.SafeString(reader, "fname"), //reader.GetString("fname"),
                        lname = SqlHelper.SafeString(reader, "lname"),    // reader.GetString("lname"),
                        target = reader.GetDateTime("target"),
                        group = SqlHelper.SafeInt(reader, "liturgy")  //reader.GetInt16("liturgy")
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
    }
}