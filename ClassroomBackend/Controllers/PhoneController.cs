using ClassroomBackend.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassroomBackend.Controllers
{
    public class PhoneController : ApiController
    {
        [HttpPut]
        //public HttpResponseMessage Put(uint pid, string phone, string label)
        public HttpResponseMessage Put(Phone phone)
        {
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var sql = new SqlHelper();
            //var result = sql.AddPhone(pid, phone, label);
            var result = sql.AddPhone(phone);
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "{}");
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(uint pid, string phone)
        {
            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var sql = new SqlHelper();
            var result = sql.DeletePhone(pid, phone);
            if (result > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "{}");
            }
            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public HttpResponseMessage Get(uint id)
        {

            var user = TokenHelper.Authorize(this.Request);
            if (user == null) return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var helper = new SqlHelper();
            var phones = helper.GetPhones(id);
            if (phones != null)
            {
                IEnumerable<Phone> responseBody = phones;
                return Request.CreateResponse(HttpStatusCode.OK, responseBody);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
    }
}