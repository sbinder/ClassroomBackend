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


            return Request.CreateResponse(HttpStatusCode.OK, "{\"token\":\"" + t + "\"}");
            //return Request.CreateResponse(HttpStatusCode.OK, "{\"" + t + "\"}");

        }
    }
}
