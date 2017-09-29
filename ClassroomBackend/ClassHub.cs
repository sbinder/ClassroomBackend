using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;


namespace ClassroomBackend
{
    public class ClassHub : Hub
    {
        public void Send(uint channel, uint stid, bool status)
        {
            Clients.All.broadcastMessage(channel, stid, status);
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}