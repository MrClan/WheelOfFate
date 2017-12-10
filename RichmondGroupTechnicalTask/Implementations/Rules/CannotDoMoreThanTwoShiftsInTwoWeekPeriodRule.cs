using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Implementations
{
    public class CannotDoMoreThanTwoShiftsInTwoWeekPeriodRule : IRule
    {
        public bool Validate(Engineer engineer, DateTime date)
        {
            var endDate = date;
            var startDate = endDate.AddDays(-15);
            // if already been scheduled more than twice in last two weeks, mark him as invalid
            // TODO: TAKE NOTE OF OFF-BY-ONE ERROR - to include or not include current date
            return !(engineer.Schedules.Where(s => s.Date.IsBetween(startDate, endDate)).Count() >= 2);
        }
    }
}