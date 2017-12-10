using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Implementations
{

    public class CannotHaveHalfDayShiftsOnConsecutiveDaysRule : IRule
    {
        public bool Validate(Engineer engineer, DateTime date)
        {
            var yesterday = date.AddDays(-1).Date;
            return !engineer.Schedules.Any(s => s.Date.Date == yesterday);
        }
    }
}