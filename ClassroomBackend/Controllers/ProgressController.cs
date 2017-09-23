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

        [HttpPut]
        public HttpResponseMessage Progress(Progress progress)
        {
            // replace with real teacher ID
            const uint teacher = 1;

            try
            {
                var today = DateTime.Now.Date.ToString("yyyy-MM-dd");
                MySqlCommand cmd = db.CreateCommand();
                cmd.CommandText = @"select count(stid) from progress where stid = @stid and taskid = @prid and date = @today";
                cmd.Parameters.AddWithValue("@stid", progress.stid);
                cmd.Parameters.AddWithValue("@prid", progress.prid);
                cmd.Parameters.AddWithValue("@today", today);

                db.Open();
                var rowCount = int.Parse(cmd.ExecuteScalar().ToString());
                if (rowCount > 0)
                {
                    // already present: update
                    var ucmd = db.CreateCommand();
                    ucmd.CommandText = @"update progress set rating=@rating, scomment=@scomment, tcomment=@tcomment where stid=@stid and taskid=@prid and date=@date";
                    ucmd.Parameters.AddWithValue("@stid", progress.stid);
                    ucmd.Parameters.AddWithValue("@prid", progress.prid);
                    ucmd.Parameters.AddWithValue("@date", today);
                    ucmd.Parameters.AddWithValue("@tid", teacher);
                    ucmd.Parameters.AddWithValue("@rating", progress.rating);
                    ucmd.Parameters.AddWithValue("@scomment", progress.scomment);
                    ucmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
                    ucmd.ExecuteNonQuery();
                    var l = new HttpResponseMessage(HttpStatusCode.OK);
                    l.Content = new StringContent("OK");
                    return l;
                }
                else
                {
                    // not present: create
                    var icmd = db.CreateCommand();
                    icmd.CommandText = @"insert into progress (stid, taskid, date, tid, rating, scomment, tcomment) values (@stid, @prid, @date, @tid, @rating, @scomment, @tcomment)";
                    icmd.Parameters.AddWithValue("@stid", progress.stid);
                    icmd.Parameters.AddWithValue("@prid", progress.prid);
                    icmd.Parameters.AddWithValue("@date", today);
                    icmd.Parameters.AddWithValue("@tid", teacher);
                    icmd.Parameters.AddWithValue("@rating", progress.rating);
                    icmd.Parameters.AddWithValue("@scomment", progress.scomment);
                    icmd.Parameters.AddWithValue("@tcomment", progress.tcomment);
                    icmd.ExecuteNonQuery();
                    var l = new HttpResponseMessage(HttpStatusCode.OK);
                    l.Content = new StringContent("OK");
                    return l;

                }

            } catch (Exception exo ) {

                // log exo - DO NOT RETURN IT!
                var l = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                l.Content = new StringContent( exo.ToString() );
                return l;
            }                    
        }
    }
}
