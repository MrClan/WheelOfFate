using RichmondGroupTechnicalTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichmondGroupTechnicalTask.Contracts
{
    public interface IRule
    {
        bool Validate(Engineer engineer, DateTime date);
    }
}