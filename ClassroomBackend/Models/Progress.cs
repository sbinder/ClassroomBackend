using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomBackend.Models
{
    public class Progress
    {
        public uint stid;
        public uint prid;
        public int rating;
        public bool assigned = false;
        public string tcomment;
        public string scomment;
        public DateTime changed;
    }
}