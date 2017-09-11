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
            var students = new List<Student>();
            try
            {
                db.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                //var columns = new List<string>();
                //for (int i = 0; i < reader.FieldCount; i++)
                //{
                //    columns.Add(reader.GetName(i));
                //}
                while (reader.Read())
                {
                    var s = new Student();
                    s.stid = reader.GetUInt32("stid");
                    s.fname = reader.GetString("fname");
                    s.lname = reader.GetString("lname");
                    s.target = reader.GetDateTime("target");
                    s.group = reader.GetInt16("liturgy");

                    students.Add(s);
                    //students.Add(reader.ToString());
                }
            } catch (Exception r)
            {
                Console.WriteLine("Error: " + r);                
            }
            //return new string[] { "value1", "value2" };
            return students;
        }
        //public string[] Students()
        //{
        //    return new string[] { "first", "second" };
        //}
       
    }
    public class Student
    {
        public uint stid;
        public string fname;
        public string lname;
        public int group;
        public DateTime target;
    }
}
