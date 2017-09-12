using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomBackend.Models
{
    public class Student
    {
        public uint stid;
        public string fname;
        public string lname;
        public int group;
        public DateTime target;

        public static List<Student> GetStudentList(MySqlDataReader reader)
        {
            var students = new List<Student>();
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
    }
}