using ClassroomBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassroomBackend
{
    public class DBEngine
    {
        private static string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

        MySqlConnection db = new MySqlConnection(connectionString);

        public string UpdateProgress(Progress progress)
        {
            if (progress == null) return "empty progress";
            try
            {
                var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = @"select count(stid) from progress where stid = @stid and taskid = @taskid and date = @today";
                cmd.Parameters.AddWithValue("@stid", progress.stid);
                cmd.Parameters.AddWithValue("@taskid", progress.taskid);
                cmd.Parameters.AddWithValue("@today", today);

                db.Open();
                var rowCount = int.Parse(cmd.ExecuteScalar().ToString());
                if (rowCount > 0)
                {
                    // already present: update
                    var ucmd = db.CreateCommand();
                    ucmd.CommandText = @"update progress set rating=@rating, scomment=@scomment, tcomment=@tcomment where stid=@stid and taskid=@taskid and date=@date";
                    ucmd.Parameters.AddWithValue("@stid", progress.stid);
                    ucmd.Parameters.AddWithValue("@taskid", progress.taskid);
                    ucmd.Parameters.AddWithValue("@date", today);
                    ucmd.Parameters.AddWithValue("@tid", progress.tid);
                    ucmd.Parameters.AddWithValue("@rating", progress.rating);
                    ucmd.Parameters.AddWithValue("@scomment", progress.scomment);
                    ucmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
                    ucmd.ExecuteNonQuery();
                    return string.Empty;
                    //var l = new HttpResponseMessage(HttpStatusCode.OK);
                    //l.Content = new StringContent("OK");
                    //return l;
                }
                else
                {
                    // not present: create
                    var icmd = db.CreateCommand();
                    icmd.CommandText = @"insert into progress (stid, taskid, date, tid, rating, scomment, tcomment) values (@stid, @taskid, @date, @tid, @rating, @scomment, @tcomment)";
                    icmd.Parameters.AddWithValue("@stid", progress.stid);
                    icmd.Parameters.AddWithValue("@taskid", progress.taskid);
                    icmd.Parameters.AddWithValue("@date", today);
                    icmd.Parameters.AddWithValue("@tid", progress.tid);
                    icmd.Parameters.AddWithValue("@rating", progress.rating);
                    icmd.Parameters.AddWithValue("@scomment", progress.scomment);
                    icmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
                    icmd.ExecuteNonQuery();
                    return string.Empty;
                    //var l = new HttpResponseMessage(HttpStatusCode.OK);
                    //l.Content = new StringContent("OK");
                    //return l;

                }

            }
            catch (Exception exo)
            {
                return exo.ToString().Substring(0, 255).Replace('\n', ' ').Replace('\r', ' ');
                //// log exo - DO NOT RETURN IT!
                //var l = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                //if (progress == null)
                //{
                //    l.ReasonPhrase = "progress is null";
                //}
                //else
                //{
                //    l.ReasonPhrase = exo.ToString().Substring(0, 255).Replace('\n', ' ').Replace('\r', ' ');
                //}
                //return l;
            }

        }
    }
}