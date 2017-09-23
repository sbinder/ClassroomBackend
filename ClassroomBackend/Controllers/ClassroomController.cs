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


        [HttpGet]
        public IEnumerable<Student> Students()
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
                        fname = reader.GetString("fname"),
                        lname = reader.GetString("lname"),
                        target = reader.GetDateTime("target"),
                        group = reader.GetInt16("liturgy")
                    };
                    students.Add(s);
                }

                return students;

            }
            catch (Exception r)
            {
                Console.WriteLine("Error: " + r);
            }
            return new List<Student>();
        }


        [HttpPost]
        public IEnumerable<Progress> Prayers(List<uint> students)
        {
            List<Progress> prayers = new List<Progress>();
            var slist = String.Join(",", students);
            MySqlCommand cmd = db.CreateCommand();
            cmd.CommandText = "select stid, prid, date, rating, scomment, tcomment from progress where stid = 1 and taskid = 2 order by date desc limit 1";
            try
            {
                db.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Progress
                    {
                        stid = reader.GetUInt32("stid"),
                        prid = reader.GetUInt32("prid"),
                        changed = reader.GetDateTime("date"),
                        rating = reader.GetInt16("rating"),
                        scomment = reader.GetString("scomment"),
                        tcomment = reader.GetString("tcomment"),
                        assigned = true
                    };
                    prayers.Add(p);
                }

            }
            catch (Exception ex)
            {
                return null;
            }


            return prayers;
        }
    }
}