using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomBackend.Models
{
    public class Progress
    {
        public uint stid;
        public uint taskid;
        public uint tid;
        public int rating;
        public string tcomment;
        public string scomment;
        public DateTime? assigned = null;
        public DateTime changed;
    }
}