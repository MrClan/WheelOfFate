using RichmondGroupTechnicalTask.Contracts;
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
        private static SchedulesRepositoryInMemory schedulesRepository = new SchedulesRepositoryInMemory();
        private static EngineersRepositoryInMemory engineersRepository = new EngineersRepositoryInMemory();


        private static List<IRule> AppliedRules
        {
            get
            {
                return new List<IRule>
                {
                    new CanDoShiftOnThisDateRule(),
                    new CannotHaveHalfDayShiftsOnConsecutiveDaysRule(),
                    new CannotDoMoreThanTwoShiftsInTwoWeekPeriodRule()
                };
            }
        }

        private static List<Engineer> GetAllEngineers()
        {
            var allEngineers = engineersRepository.GetMany(e => true);// load all engineers
            return allEngineers.ToList();
        }

        public static Dictionary<int, List<Engineer>> RotateTheWheelOfFate()
        {
            DateTime forDate = DateTime.Now.Date; // throw away time part
            var tableOfFate = new Dictionary<int, List<Engineer>>();

            var allEngineers = GetAllEngineers();
            var allSchedules = new List<Schedule>();
            pickedEngineers = new HashSet<Guid>();

            for (int i = 0; i < 10; i++)
            {
                forDate = forDate.AddDays(1);
                // ensure every engineer has appeared atleast once before 9th day
                if (i == 7)
                {
                    pickRandomly = allEngineers.Where(e => !pickedEngineers.Contains(e.Id)).Count() == 0;
                }

                var engineer1 = GetValidEngineersForTheDay(allEngineers, allSchedules, forDate);
                var engineer2 = GetValidEngineersForTheDay(allEngineers, allSchedules, forDate);

                tableOfFate.Add(i, new List<Engineer> { engineer1, engineer2 });
            }
            return tableOfFate;
        }

        static HashSet<Guid> pickedEngineers = new HashSet<Guid>();
        static bool pickRandomly = true;

        private static Engineer GetValidEngineersForTheDay(List<Engineer> allEngineers, List<Schedule> allSchedules, DateTime forDate)
        {
            Engineer selectedEngineer = null;

            // pick one engineer at random, check if they pass all the business rules
            // if they do, return that engineer
            // if not, repeat the process until all engineers are checked

            HashSet<int> checkedIndices = new HashSet<int>();

            for (int i = 0; i < allEngineers.Count; i++) // worst case scenario, this loop will fully be executed
            {
                if (pickRandomly)
                {
                    var randomIndex = 0;
                    do
                    {
                        randomIndex = _randomizer.Next() % allEngineers.Count;
                    } while (checkedIndices.Contains(randomIndex)); // make sure same engineer is not checked over and over again
                    checkedIndices.Add(randomIndex);
                    selectedEngineer = allEngineers[randomIndex];
                }
                else
                {
                    var remainingEngineers = allEngineers.Where(e => !pickedEngineers.Contains(e.Id));
                    selectedEngineer = allEngineers.First(e => e.Id == remainingEngineers.First().Id);
                    pickRandomly = true;
                }
                selectedEngineer.Schedules = allSchedules.Where(s => s.EngineerId == selectedEngineer.Id).ToList();

                var isEligible = AppliedRules.All(r => r.Validate(selectedEngineer, forDate));
                if (isEligible)
                {
                    // add this schedule to database
                    var schedule = new Schedule() { EngineerId = selectedEngineer.Id, Date = forDate, Shift = Shift.First };
                    schedulesRepository.SaveOne(schedule);
                    allSchedules.Add(schedule);
                    pickedEngineers.Add(selectedEngineer.Id);
                    break;
                }
            }


            return selectedEngineer;
        }
    }
}