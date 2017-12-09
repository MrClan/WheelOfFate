using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Contracts
{
    public interface IDbEntity
    {
        Guid Id { get; set; }
    }
}