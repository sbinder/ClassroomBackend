using ClassroomBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassroomBackend.Controllers
{
    public class ParentController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string namepart)
        {
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var helper = new SqlHelper();
            var parents = helper.FindParent(user.org, namepart);
            if (parents == null)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            IEnumerable<Parent> responseBody = parents;
            return Request.CreateResponse(HttpStatusCode.OK, responseBody);
        }
    }
}