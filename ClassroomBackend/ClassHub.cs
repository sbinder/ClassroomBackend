using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;

namespace ClassroomBackend
{
    public class ClassHub : Hub
    {
        public void Send(uint channel, uint stid, bool status)
        {
//            if (LogAttendance(stid, status))
                Clients.All.broadcastMessage(channel, stid, status);
        }

        public void Progress(uint channel, uint stid, uint tid, uint taskid, int rating, string tcomment, string scomment)
        {
            //update db

            Clients.All.broadcastProgress(channel, stid, tid, taskid, rating, tcomment, scomment);
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        private bool LogAttendance(uint stid, bool status)
        {
            string connectionString = "server=localhost;user id = mcuser; password = xT87$nXIaZf0; persistsecurityinfo=True;database=mc";

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
                return false;
            }
            return true;
        }
    }
}