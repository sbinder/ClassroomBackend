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
        public uint parent;
        public uint teacher;
        public string fname;
        public string lname;
        public string email;
        public int group;
        public DateTime target;
        public string torah;
        public string haftara;
        public bool present;
        public string note;
        public string username;
        public string password;
        public bool male;
        public bool trial;
    }
}