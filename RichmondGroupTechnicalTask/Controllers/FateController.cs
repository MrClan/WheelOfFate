using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RichmondGroupTechnicalTask.Controllers
{
    public class FateController : ApiController
    {
        public IHttpActionResult Get()
        {
            // return a table for 10 days (2 weeks, with 5 working days each)
            var tableOfFate = new Dictionary<int, List<Engineer>>();

            tableOfFate = AppCore.RotateTheWheelOfFate();
            return Ok(tableOfFate.Select(kvp => $"{kvp.Key}: {String.Join(",", kvp.Value.Select(e=> e.Name))}"));
        }
    }
}
