using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichmondGroupTechnicalTask.Implementations;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTT.Tests
{
    [TestClass]
    public class RulesTests
    {
        [TestMethod]
        public void CanDoShiftOnThisDateRule()
        {
            var engineerId = Guid.NewGuid();
            var date = DateTime.Now.Date;
            var overworkingEngineer = new Engineer()
            {
                Id = engineerId,
                Name = "OverworkingEngineer",
                Schedules = new List<Schedule>()
                {
                    new Schedule{ Id = Guid.NewGuid(), Date = date, EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date, EngineerId = engineerId, Shift = Shift.Second}
                }
            };

            var ruleUnderTest = new CanDoShiftOnThisDateRule();
            var result = ruleUnderTest.Validate(overworkingEngineer, date);

            Assert.IsFalse(result, "Returned TRUE, when should have returned false");

            engineerId = Guid.NewGuid();
            date = DateTime.Now.Date;
            var properWorkingEngineer = new Engineer()
            {
                Id = engineerId,
                Name = "ProperWorkingEngineer",
                Schedules = new List<Schedule>()
                {
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(2), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(1), EngineerId = engineerId, Shift = Shift.Second}
                }
            };

            ruleUnderTest = new CanDoShiftOnThisDateRule();
            result = ruleUnderTest.Validate(properWorkingEngineer, date);

            Assert.IsTrue(result, "Returned FALSE, when should have returned true");
        }

        [TestMethod]
        public void CannotHaveHalfDayShiftsOnConsecutiveDaysRule()
        {
            var engineerId = Guid.NewGuid();
            var date = DateTime.Now.Date;
            var overworkingEngineer = new Engineer()
            {
                Id = engineerId,
                Name = "OverworkingEngineer",
                Schedules = new List<Schedule>()
                {
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-1), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date, EngineerId = engineerId, Shift = Shift.Second}
                }
            };

            var ruleUnderTest = new CannotHaveHalfDayShiftsOnConsecutiveDaysRule();
            var result = ruleUnderTest.Validate(overworkingEngineer, date);

            Assert.IsFalse(result, "Returned TRUE, when should have returned false");

            engineerId = Guid.NewGuid();
            date = DateTime.Now.Date;
            var properWorkingEngineer = new Engineer()
            {
                Id = engineerId,
                Name = "ProperWorkingEngineer",
                Schedules = new List<Schedule>()
                {
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(2), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(1), EngineerId = engineerId, Shift = Shift.Second}
                }
            };

            ruleUnderTest = new CannotHaveHalfDayShiftsOnConsecutiveDaysRule();
            result = ruleUnderTest.Validate(properWorkingEngineer, date);

            Assert.IsTrue(result, "Returned FALSE, when should have returned true");
        }

        [TestMethod]
        public void HaveAlreadyBeenScheduledTwiceInLastTwoWeekPeriodRule()
        {
            var engineerId = Guid.NewGuid();
            var date = DateTime.Now.Date;
            var overworkingEngineer = new Engineer()
            {
                Id = engineerId,
                Name = "OverworkingEngineer",
                Schedules = new List<Schedule>()
                {
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-1), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-20), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-30), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-40), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-50), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-60), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-70), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-80), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-90), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-100), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-110), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-120), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-130), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-140), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-150), EngineerId = engineerId, Shift = Shift.First}
                }
            };

            var ruleUnderTest = new CannotDoMoreThanTwoShiftsInTwoWeekPeriodRule();
            var result = ruleUnderTest.Validate(overworkingEngineer, date);

            Assert.IsFalse(result, "Returned TRUE, when should have returned false");

            engineerId = Guid.NewGuid();
            date = DateTime.Now.Date;
            var properWorkingEngineer = new Engineer()
            {
                Id = engineerId,
                Name = "ProperWorkingEngineer",
                Schedules = new List<Schedule>()
                {
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-1), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-20), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-30), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-4), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-50), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-60), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-70), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-80), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-90), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-100), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-110), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-120), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-130), EngineerId = engineerId, Shift = Shift.First},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-140), EngineerId = engineerId, Shift = Shift.Second},
                    new Schedule{ Id = Guid.NewGuid(), Date = date.AddDays(-150), EngineerId = engineerId, Shift = Shift.First}
                }
            };

            ruleUnderTest = new CannotDoMoreThanTwoShiftsInTwoWeekPeriodRule();
            result = ruleUnderTest.Validate(properWorkingEngineer, date);

            Assert.IsTrue(result, "Returned FALSE, when should have returned true");
        }
    }
}
