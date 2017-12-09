using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RichmondGroupTechnicalTask.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "yo, the api is up bro";
        }
    }
}
