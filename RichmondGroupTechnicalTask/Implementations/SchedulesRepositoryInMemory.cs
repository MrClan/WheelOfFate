using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Implementations
{
    public class SchedulesRepositoryInMemory : IRepository<Schedule>
    {
        RGTTDbContextInMemory context = new RGTTDbContextInMemory();

        public ICollection<Schedule> GetMany(Func<Schedule, bool> filterLambda)
        {
            return context.Schedules.Where(filterLambda).ToList();
        }        

        public Guid SaveOne(Schedule entityToSave)
        {
            entityToSave.Id = Guid.NewGuid();
            context.Schedules.Add(entityToSave);
            return entityToSave.Id;
        }

        public Schedule GetOne(Guid Id)
        {
            return context.Schedules.FirstOrDefault(e => e.Id == Id);
        }
    }
}