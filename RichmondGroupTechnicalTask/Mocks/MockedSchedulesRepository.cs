using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Mocks
{
    public class MockedSchedulesRepository : IRepository<Schedule>
    {
        private List<Schedule> _schedules = new List<Schedule>();

        public MockedSchedulesRepository()
        {
            var engineersRepository = new MockedEngineersRepository();
            var allEngineers = engineersRepository.GetMany(e => true);

            foreach (var eng in allEngineers)
            {
                //for (int i = 0; i < 50; i++)
                //{
                //    _schedules.Add(new Schedule(DateTime.Now.AddDays(-1* i), Shift.First, eng.Id));
                //}
            }
            
        }

        public ICollection<Schedule> GetMany(Func<Schedule, bool> filterLambda)
        {
            return _schedules.Where(filterLambda).ToList();
        }

        public Schedule GetOne(Guid Id)
        {
            return _schedules.FirstOrDefault(e => e.Id == Id);
        }

        public Guid SaveOne(Schedule ScheduleToSave)
        {
            var newGuid = Guid.NewGuid();
            ScheduleToSave.Id = newGuid;
            _schedules.Add(ScheduleToSave);
            return newGuid;
        }
    }
}