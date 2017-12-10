using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Implementations
{
    public class CanDoShiftOnThisDateRule : IRule
    {
        /// <summary>
        /// Returns true only if the passed engineer is eligible to do a shift on the given date
        /// </summary>
        public bool Validate(Engineer engineer, DateTime date) => !engineer.Schedules.Any(s => s.Date.Date == date.Date);
    }
}