using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RichmondGroupTechnicalTask.Controllers
{
    public class FateController : ApiController
    {
        public IEnumerable<Engineer> Get(DateTime forDate)
        {
            if (forDate == null) forDate = DateTime.Now.Date;
            return AppCore.RotateTheWheelOfFate(forDate);
        }
    }
}
