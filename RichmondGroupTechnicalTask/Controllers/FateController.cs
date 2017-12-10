using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RichmondGroupTechnicalTask.Controllers
{
    [EnableCors("*","*","*")]
    public class FateController : ApiController
    {
        public IHttpActionResult Get()
        {
            // return a table for 10 days (2 weeks, with 5 working days each)
            var tableOfFate = new Dictionary<int, List<Engineer>>();
            tableOfFate = AppCore.RotateTheWheelOfFate();
            return Ok(tableOfFate.Select(kvp => kvp.Value.Select(e=> e.Name)));
        }
    }
}
