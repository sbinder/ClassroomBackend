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

        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        MySqlConnection db = new MySqlConnection(connectionString);

        // post returns students in attendance
        [HttpPost]
        public HttpResponseMessage Post()
        {
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            SqlHelper sql = new SqlHelper();
            try
            {
                var slist = sql.StudentList(user.org);  // all students
                var students = new List<Student>();                
                foreach (var s in slist)
                {
                    if (s.present) { students.Add(s); }
                    // if (!s.present) { slist.Remove(s); }
                }
                IEnumerable<Student> responseBody = students;
                return Request.CreateResponse(HttpStatusCode.OK, responseBody);
            } catch (Exception e)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);

                // DO NOT DO THIS IN PRODUCTION!
                var replacement = e.ToString().Replace('\n', '*').Replace('\r', '*').Substring(0, 255); //Regex.Replace(r.ToString(), @"\t|\n|\r", "*");
                response.ReasonPhrase = replacement;

                // TODO: Log error
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage Get(string namepart)
        {
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var helper = new SqlHelper();
            var students = helper.FindStudent(user.org, namepart);
            return ReturnStudents(0, students);
        }

        [HttpGet]
        public HttpResponseMessage Get(uint id = 0)
        {
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var helper = new SqlHelper();
            var students = helper.StudentList(user.org, id);

            return ReturnStudents(id, students);

        }

        private HttpResponseMessage ReturnStudents(uint id, List<Student> students)
        {
            if (students == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            if (id == 0)
            {
                IEnumerable<Student> responseBody = students;
                return Request.CreateResponse(HttpStatusCode.OK, responseBody);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, students[0]);
            }
        }
    }
}