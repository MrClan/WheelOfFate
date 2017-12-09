using RichmondGroupTechnicalTask.Implementations;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask
{
    public class AppCore
    {

        private static Random _randomizer = new Random();
        private static SchedulesRepository schedulesRepository = new SchedulesRepository();
        private static EngineersRepository engineersRepository = new EngineersRepository();

        private static List<IRule> AppliedRules
        {
            get
            {
                return new List<IRule>
                {
                    new CannotDoTwoShiftsOnTheSameDayRule(),
                    new CannotHaveHalfDayShiftsOnConsecutiveDaysRule(),
                    new HaveAlreadyBeenScheduledTwiceInLastTwoWeekPeriodRule()
                };
            }
        }

        private static List<Engineer> GetAllEngineers()
        {
            var allEngineers = engineersRepository.GetMany(e => true);// load all engineers
            return allEngineers.ToList();
        }

        public static List<Schedule> GetBiWeeklySchedule(DateTime forDate)
        {
            var endDate = forDate;
            var startDate = endDate.AddDays(-14); // since we're considering only weekdays
            var schedules = new List<Schedule>();
            schedules = schedulesRepository.GetMany(s => s.Date.IsBetween(startDate, endDate)).ToList();
            return schedules;
        }

        public static List<Engineer> RotateTheWheelOfFate(DateTime forDate)
        {
            forDate = forDate.Date; // throw away time part
            // check if schedules for forDate has already been calculated, if so return that
            var schedulesForToday = schedulesRepository.GetMany(s => s.Date == forDate.Date).ToList();
            if (schedulesForToday.Count == 2)
            {
                return engineersRepository.GetMany(e => e.Id == schedulesForToday[0].EngineerId || e.Id == schedulesForToday[1].EngineerId).ToList();
            }


            // 1. get all engineers, with their schedules for past two weeks
            // 2. validate rules
            // 3. return a pair of engineers

            var allEngineers = GetAllEngineers();
            var allSchedules = GetBiWeeklySchedule(forDate);

            var engineer1 = GetValidEngineersForTheDay(allEngineers, allSchedules, forDate);
            var engineer2 = GetValidEngineersForTheDay(allEngineers, allSchedules, forDate);

            return new List<Engineer> { engineer1, engineer2 };
        }

        private static Engineer GetValidEngineersForTheDay(List<Engineer> allEngineers, List<Schedule> allSchedules, DateTime forDate)
        {
            Engineer selectedEngineer = null;

            // pick one engineer at random, check if they pass all the business rules
            // if they do, return that engineer
            // if not, repeat the process until all engineers are checked

            HashSet<int> checkedIndices = new HashSet<int>();

            for (int i = 0; i < allEngineers.Count; i++) // worst case scenario, this loop will fully be executed
            {
                var randomIndex = 0;
                do
                {
                    randomIndex = _randomizer.Next() % allEngineers.Count;
                } while (checkedIndices.Contains(randomIndex)); // make sure same engineer is not checked over and over again
                checkedIndices.Add(randomIndex);
                selectedEngineer = allEngineers[randomIndex];
                selectedEngineer.Schedules = allSchedules.Where(s => s.EngineerId == selectedEngineer.Id).ToList();

                var isValid = AppliedRules.All(r => r.Validate(selectedEngineer, forDate));
                if (isValid)
                {
                    // add this schedule to database
                    var schedule = new Schedule() { EngineerId = selectedEngineer.Id, Date = forDate, Shift = Shift.First };
                    schedulesRepository.SaveOne(schedule);
                    allSchedules.Add(schedule);
                    break;
                }
            }


            return selectedEngineer;
        }
    }
}