using RichmondGroupTechnicalTask.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Models
{
    public class Schedule : IDbEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Shift Shift { get; set; }
        public Guid EngineerId { get; set; }
        public virtual Engineer Engineer { get; set; }
    }



    public class BiWeeklySchedule
    {
        List<Schedule> lastTwoWeeksSchedule = new List<Schedule>(10);

    }

    public interface IRule
    {
        bool Validate(Engineer engineer, DateTime date);
    }

    public class CannotDoTwoShiftsOnTheSameDayRule : IRule
    {
        public bool Validate(Engineer engineer, DateTime date)
        {
            return !engineer.Schedules.Any(s => s.Date.Date == date.Date);
        }
    }

    public class CannotHaveHalfDayShiftsOnConsecutiveDaysRule : IRule
    {
        public bool Validate(Engineer engineer, DateTime date)
        {
            var yesterday = date.AddDays(-1).Date;
            return !engineer.Schedules.Any(s=> s.Date.Date == yesterday);
        }
    }

    public class HaveAlreadyBeenScheduledTwiceInLastTwoWeekPeriodRule : IRule
    {
        public bool Validate(Engineer engineer, DateTime date)
        {
            var endDate = date;
            var startDate = endDate.AddDays(-15);
            // if already been scheduled more than twice in last two weeks, mark him as invalid
            // TODO: TAKE NOTE OF OFF-BY-ONE ERROR - to include or not include current date
            return engineer.Schedules.Where(s=> s.Date.IsBetween(startDate, endDate)).Count() < 2;
        }
    }
}