using RichmondGroupTechnicalTask.Contracts;
using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Mocks
{
    public class MockedEngineersRepository : IRepository<Engineer>
    {
        private List<Engineer> _engineers = new List<Engineer>();

        public MockedEngineersRepository()
        {
            for (int i = 0; i < 10; i++)
            {
                _engineers.Add(new Engineer { Id = Guid.NewGuid(), Name = $"Engineer_{i}" });
            }
        }

        public ICollection<Engineer> GetMany(Func<Engineer, bool> filterLambda) => _engineers.Where(filterLambda).ToList();

        public Engineer GetOne(Guid Id) => _engineers.FirstOrDefault(e => e.Id == Id);

        public Guid SaveOne(Engineer engineerToSave)
        {
            var newGuid = Guid.NewGuid();
            engineerToSave.Id = newGuid;
            _engineers.Add(engineerToSave);
            return newGuid;
        }
    }
}