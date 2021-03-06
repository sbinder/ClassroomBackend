﻿using MySql.Data.MySqlClient;
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
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        MySqlConnection db = new MySqlConnection(connectionString);




        [HttpPost]
        //public IEnumerable<Progress> Prayers(List<uint> students)
        public HttpResponseMessage Prayers(List<uint> students)
        {

            var user = TokenHelper.Authorize(this.Request);        
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

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
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var helper = new SqlHelper();
            var err = helper.UpdateProgress(progress);
            if (err == string.Empty)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            var eresp = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            eresp.ReasonPhrase = err;
            return eresp;
        }
    }
}
