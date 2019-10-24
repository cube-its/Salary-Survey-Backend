using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Repositories
{
    public interface IEmployerRepository : IRepository<Employer>
    {
        /// <summary>
        /// Gets all employers.
        /// </summary>
        /// <param name="filter">Filter by employer name</param>
        /// <returns>List of employers</returns>
        IEnumerable<DetailedEmployer> GetEmployers(string filter = null);
    }
}
