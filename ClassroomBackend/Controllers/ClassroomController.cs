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

        private string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        //[HttpGet]
        public IEnumerable<Student> Get()
        {
            MySqlConnection db = new MySqlConnection(connectionString);
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
                        fname = reader.GetString("fname"),
                        lname = reader.GetString("lname"),
                        target = reader.GetDateTime("target"),
                        group = reader.GetInt16("liturgy")
                    };
                    students.Add(s);
                }

                return students;

            } catch (Exception r)
            {
                Console.WriteLine("Error: " + r);                
            }
            return new List<Student>();
        }
    }
}
