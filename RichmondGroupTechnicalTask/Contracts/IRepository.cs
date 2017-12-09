using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Contracts
{
    public interface IRepository<T> where T : IDbEntity
    {
        T GetOne(Guid Id);

        ICollection<T> GetMany(Func<T, bool> filterLambda);

        Guid SaveOne(T entityToSave);

    }
}