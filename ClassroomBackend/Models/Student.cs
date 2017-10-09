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
        public bool present;
    }
}