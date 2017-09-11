using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClassroomBackend.Controllers
{
    public class ClassroomController : ApiController
    {
        //[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        //public string[] Students()
        //{
        //    return new string[] { "first", "second" };
        //}
    }
}
