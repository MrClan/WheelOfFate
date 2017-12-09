using RichmondGroupTechnicalTask.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Models
{
    public class Engineer : IDbEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }

        //private List<Schedule> _schedules = new List<Schedule>();
        //public List<Schedule> ScheduleForPastTwoWeeks()
        //{
        //    return _schedules;
        //}
        //public bool UpdateSchedule(Schedule schedule)
        //{
        //    // validate and add to schedules collection
        //    _schedules.Add(schedule);
        //    return true;
        //}
    }
}