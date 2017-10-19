using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using ClassroomBackend.Models;
using System.Threading.Tasks;
using System.IO;

namespace ClassroomBackend
{
    public class ClassHub : Hub
    {
        public Task JoinGroup(uint orgid)
        {
            return Groups.Add(Context.ConnectionId, orgid.ToString());
        }

        public void Checkin(uint orgid, uint stid, bool status)
        {
            if (LogAttendance(stid, status))
            {
                Clients.Group(orgid.ToString()).broadcastCheckin(stid, status);
            } else
            {
                Log("DB reported problem.");
            }
        }

        public void ProgressUpdate(uint orgid, Progress progress)
        {
            // update DB
            SqlHelper helper = new SqlHelper();
            var e = helper.UpdateProgress(progress);
            if (e == string.Empty)
                // fix date:
                if (progress.assigned == null || progress.assigned < new DateTime(1900,1,1))
                {
                    progress.assigned = DateTime.Now.Date;                    
                }
                progress.changed = DateTime.Now.Date;
            //Clients.OthersInGroup(orgid.ToString()).broadcastProgress(progress);
            Clients.Group(orgid.ToString()).broadcastProgress(progress);
        }
        public void Hello()
        {
            Clients.All.hello();
        }

        // change to return ORGID?
        private bool LogAttendance(uint stid, bool status)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

            MySqlConnection db = new MySqlConnection(connectionString);
            MySqlCommand cmd = db.CreateCommand();

            cmd.CommandText = @"insert into attendance (stid, status) values (@stid, @status)";
            cmd.Parameters.AddWithValue("@stid", stid);
            cmd.Parameters.AddWithValue("@status", status);
            try
            {
                db.Open();
                cmd.ExecuteNonQuery();
            } catch (Exception e)
            {
                // log exception               

                db.Close();
                return false;
            }
            db.Close();
            return true;
        }
        private void Log(string msg)
        {
            //using (StreamWriter w = File.AppendText(@"c:\Log\log.txt"))
            //{
            //    w.Write("\r\nLog Entry : ");
            //    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            //        DateTime.Now.ToLongDateString());
            //    w.WriteLine("  :");
            //    w.WriteLine("  :{0}", msg);
            //    w.WriteLine("-------------------------------");
            //}
        }
    }
}