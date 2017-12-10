using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask
{
    public class RGTTDbContextInMemory
    {
        public RGTTDbContextInMemory()
        {
            Engineers = new List<Engineer>();
            Schedules = new List<Schedule>();


            int engineerCount = 10;
            for (int i = 0; i < engineerCount; i++) Engineers.Add(new Models.Engineer { Id = Guid.NewGuid(), Name = $"Engineer_{i}" });
        }

        public List<Engineer> Engineers { get; private set; }
        public List<Schedule> Schedules { get; private set; }
    }
}