using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;

namespace ClassroomBackend.Models
{
    public class User
    {
        public uint tid;
        public uint org;
        public string fname;
        public string lname;
        public string email;
        public string username;
        public string pw = string.Empty;

        public User() { }
        public User(JwtPayload payload)
        {
            org = uint.Parse(payload["org"].ToString());
        }

    }
}