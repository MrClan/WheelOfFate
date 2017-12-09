using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask
{
    public class RGTTDbContext: DbContext
    {
        public RGTTDbContext():base(ConfigurationManager.ConnectionStrings["MainDbConnectionString"].ConnectionString)
        { }

        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}