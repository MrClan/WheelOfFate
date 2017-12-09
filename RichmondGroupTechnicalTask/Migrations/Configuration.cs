namespace RichmondGroupTechnicalTask.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RichmondGroupTechnicalTask.RGTTDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RichmondGroupTechnicalTask.RGTTDbContext context)
        {
            var engineerCount = context.Engineers.Count();
            if (engineerCount > 0) return;

            engineerCount = 10;
            for (int i = 0; i < engineerCount; i++) context.Engineers.Add(new Models.Engineer { Name = $"Engineer_{i}" });
            context.SaveChanges();

            //var twoEngineers = context.Engineers.Take(2).ToList();
            //var firstEngineer = twoEngineers[0];
            //context.Schedules.Add(new Models.Schedule(DateTime.Now.AddDays(-14), Models.Shift.First, firstEngineer.Id));
            //context.Schedules.Add(new Models.Schedule(DateTime.Now.AddDays(-10), Models.Shift.First, firstEngineer.Id));
            //context.Schedules.Add(new Models.Schedule(DateTime.Now.AddDays(-7), Models.Shift.Second, firstEngineer.Id));

            //var secondEngineer = twoEngineers[1];
            //context.Schedules.Add(new Models.Schedule(DateTime.Now.AddDays(-14), Models.Shift.First, secondEngineer.Id));
            //context.Schedules.Add(new Models.Schedule(DateTime.Now.AddDays(-10), Models.Shift.First, secondEngineer.Id));
            //context.Schedules.Add(new Models.Schedule(DateTime.Now.AddDays(-7), Models.Shift.Second, secondEngineer.Id));

            //context.SaveChanges();
        }
    }
}
