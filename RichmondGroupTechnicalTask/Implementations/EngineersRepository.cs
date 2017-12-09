using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Implementations
{
    public class EngineersRepository : IRepository<Engineer>
    {
        RGTTDbContext context = new RGTTDbContext();

        public ICollection<Engineer> GetMany(Func<Engineer, bool> filterLambda)
        {
            return context.Engineers.Where(filterLambda).ToList();
        }

        public Guid SaveOne(Engineer entityToSave)
        {
            context.Engineers.Add(entityToSave);
            return entityToSave.Id;
        }

        Engineer IRepository<Engineer>.GetOne(Guid Id)
        {
            return context.Engineers.FirstOrDefault(e => e.Id == Id);
        }
    }
}