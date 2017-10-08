using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClassroomBackend
{
    public partial class index : System.Web.UI.Page
    {
        public string StudentList = "[";
        protected void Page_Load(object sender, EventArgs e) {
            SqlHelper sql = new SqlHelper();            
            var slist = sql.StudentList();            
            foreach (var stud in slist)
            {
                if (StudentList.Length > 5) { StudentList += ","; }
                StudentList += @"{""s"":" + stud.stid +
                    @",""d"":""" + stud.target.ToString("yyyyMMdd") + @"""," +
                    @"""n"":""" + stud.lname + ", " + stud.fname + @"""}";
            }
            StudentList += "]";

            //StudentList = @"[{""s"":1,""d"":""20170108"",""n"":""John, Elton""},{""s"":2,""d"":""20170108"",""n"":""Joel, Billy""},{""s"":3,""d"":""20170101"",""n"":""Idol, Billy""}]";
        }
    }
}