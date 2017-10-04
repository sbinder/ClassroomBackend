﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using ClassroomBackend.Models;
using System.Threading.Tasks;

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
                Clients.All.broadcastMessage(orgid, stid, status);
        }

        public void ProgressUpdate(Progress progress)
        {
            // update DB

            Clients.All.broadcastProgress(progress);
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