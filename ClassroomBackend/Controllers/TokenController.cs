using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ClassroomBackend.Controllers
{
    public class TokenController : ApiController
    {
        [AllowAnonymous]
        public HttpResponseMessage Get(string orgid, string un, string pw)
        {
            var sql = new SqlHelper();
            var user = sql.Authorize(orgid, un, pw);

            if (user == null) return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, string.Empty);

            var helper = new TokenHelper();
            var t = helper.CreateToken(new JwtPayload
            {
                {"org", user.org },
                {"tid", user.tid },
                {"fn", user.fname }
            });


            return Request.CreateResponse(HttpStatusCode.OK, "{\"Token\":\"" + t + "\"}");

            //var pl = helper.DecodeToken(t);
            //var a = helper.FromPayload(pl, "org");
            //var b = helper.FromPayload(pl, "tid");
            //var c = helper.FromPayload(pl, "fn");

            //var str = "Token read.<br />";
            //if (a == null) str += "org is NULL.<br />";
            //else str += "org is " + a.ToString() + "<br />";

            //if (b == null) str += "tid is NULL.<br />";
            //else str += "tid is " + b.ToString() + "<br />";

            //if (c == null) str += "fn is NULL.<br />";
            //else str += "fn is " + c.ToString() + "<br />";

        }
    }
}
