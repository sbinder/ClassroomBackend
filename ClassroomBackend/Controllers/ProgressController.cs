using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Cors;
using ClassroomBackend.Models;


namespace ClassroomBackend.Controllers
{
    public class ProgressController : ApiController
    {
        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);




        [HttpPost]
        //public IEnumerable<Progress> Prayers(List<uint> students)
        public HttpResponseMessage Prayers(List<uint> students)
        {
            List<Progress> prayers = new List<Progress>();
            var slist = String.Join(",", students);
            MySqlCommand cmd = db.CreateCommand();

            cmd.CommandText = @"SELECT o.* FROM `progress` o LEFT JOIN `progress` b " +
                " ON o.stid = b.stid AND o.taskid = b.taskid AND o.date < b.date " +
                "WHERE o.stid in (" + slist + ") and b.date is NULL";

            try
            {
                db.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Progress
                    {
                        stid = reader.GetUInt32("stid"),
                        taskid = reader.GetUInt32("taskid"),
                        changed = reader.GetDateTime("date"),
                        rating = SqlHelper.SafeInt(reader, "rating"),
                        scomment = SqlHelper.SafeString(reader, "scomment"),
                        tcomment = SqlHelper.SafeString(reader, "tcomment"),
                        // assigned = true
                    };
                    prayers.Add(p);
                }
                reader.Close();
                cmd.CommandText = @"select date from progress where stid = @stid and taskid = @taskid order by date limit 1";
                foreach (var p in prayers)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@stid", p.stid);
                    cmd.Parameters.AddWithValue("@taskid", p.taskid);
                    var d = cmd.ExecuteScalar();
                    if (d != null) { p.assigned = (DateTime)d; }
                }

                IEnumerable<Progress> responseBody = prayers;
                return Request.CreateResponse(HttpStatusCode.OK, responseBody);
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                // DO NOT DO THIS IN PRODUCTION!
                var replacement = ex.ToString().Replace('\n', '*').Replace('\r', '*').Substring(0, 255);
                response.ReasonPhrase = replacement;
                return response;
            }
        }


        [HttpPut]
        public HttpResponseMessage Progress(Progress progress)
        {
            // replace with real teacher ID
            progress.tid = 1;
            var engine = new DBEngine();
            var response = engine.UpdateProgress(progress);
            HttpResponseMessage l;
            if (response.Length > 0)
            {
                l = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                {
                    l.ReasonPhrase = response;
                }
            }
            l = new HttpResponseMessage(HttpStatusCode.OK);
            return l;

            //const uint teacher = 1;
            //try
            //{
            //    var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
            //    MySqlCommand cmd = db.CreateCommand();
            //    cmd.CommandText = @"select count(stid) from progress where stid = @stid and taskid = @taskid and date = @today";
            //    cmd.Parameters.AddWithValue("@stid", progress.stid);
            //    cmd.Parameters.AddWithValue("@taskid", progress.taskid);
            //    cmd.Parameters.AddWithValue("@today", today);

            //    db.Open();
            //    var rowCount = int.Parse(cmd.ExecuteScalar().ToString());
            //    if (rowCount > 0)
            //    {
            //        // already present: update
            //        var ucmd = db.CreateCommand();
            //        ucmd.CommandText = @"update progress set rating=@rating, scomment=@scomment, tcomment=@tcomment where stid=@stid and taskid=@taskid and date=@date";
            //        ucmd.Parameters.AddWithValue("@stid", progress.stid);
            //        ucmd.Parameters.AddWithValue("@taskid", progress.taskid);
            //        ucmd.Parameters.AddWithValue("@date", today);
            //        ucmd.Parameters.AddWithValue("@tid", teacher);
            //        ucmd.Parameters.AddWithValue("@rating", progress.rating);
            //        ucmd.Parameters.AddWithValue("@scomment", progress.scomment);
            //        ucmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
            //        ucmd.ExecuteNonQuery();
            //        var l = new HttpResponseMessage(HttpStatusCode.OK);
            //        l.Content = new StringContent("OK");
            //        return l;
            //    }
            //    else
            //    {
            //        // not present: create
            //        var icmd = db.CreateCommand();
            //        icmd.CommandText = @"insert into progress (stid, taskid, date, tid, rating, scomment, tcomment) values (@stid, @taskid, @date, @tid, @rating, @scomment, @tcomment)";
            //        icmd.Parameters.AddWithValue("@stid", progress.stid);
            //        icmd.Parameters.AddWithValue("@taskid", progress.taskid);
            //        icmd.Parameters.AddWithValue("@date", today);
            //        icmd.Parameters.AddWithValue("@tid", teacher);
            //        icmd.Parameters.AddWithValue("@rating", progress.rating);
            //        icmd.Parameters.AddWithValue("@scomment", progress.scomment);
            //        icmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
            //        icmd.ExecuteNonQuery();
            //        var l = new HttpResponseMessage(HttpStatusCode.OK);
            //        l.Content = new StringContent("OK");
            //        return l;

            //    }

            //} catch (Exception exo ) {

            //    // log exo - DO NOT RETURN IT!
            //    var l = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            //    if (progress == null) {
            //        l.ReasonPhrase = "progress is null";
            //    } else {
            //        l.ReasonPhrase = exo.ToString().Substring(0, 255).Replace('\n', ' ').Replace('\r', ' ');
            //        }
            //    return l;
            //}                    
        }
    }
}
