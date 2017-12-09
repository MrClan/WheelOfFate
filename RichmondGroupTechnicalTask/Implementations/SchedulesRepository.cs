using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Implementations
{
    public class SchedulesRepository : IRepository<Schedule>
    {
        RGTTDbContext context = new RGTTDbContext();

        public ICollection<Schedule> GetMany(Func<Schedule, bool> filterLambda)
        {
            return context.Schedules.Where(filterLambda).ToList();
        }        

        public Guid SaveOne(Schedule entityToSave)
        {
            context.Schedules.Add(entityToSave);
            context.SaveChanges();
            return entityToSave.Id;
        }

        public Schedule GetOne(Guid Id)
        {
            return context.Schedules.FirstOrDefault(e => e.Id == Id);
        }
    }
}