using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Services
{
    /// <summary>
    /// Employer service interface that exposes the relevant operations.
    /// </summary>
    public interface IEmployerService
    {
        /// <summary>
        /// Gets all employers.
        /// </summary>
        /// <param name="filter">Filter by employer name</param>
        /// <returns>List of employers</returns>
        IEnumerable<DetailedEmployer> GetEmployers(string filter = null);
    }
}
