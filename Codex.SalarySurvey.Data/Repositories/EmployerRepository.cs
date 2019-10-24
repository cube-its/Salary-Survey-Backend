using Codex.SalarySurvey.Data.Infrastructure;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codex.SalarySurvey.Data.Repositories
{
    public class EmployerRepository : RepositoryBase<Employer>, IEmployerRepository
    {
        public EmployerRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public IEnumerable<DetailedEmployer> GetEmployers(string filter = null)
        {
            return (from a in DbContext.Employers
                    where (string.IsNullOrEmpty(filter)
                    || a.EmployerOriginalNameHeb.StartsWith(filter))
                    && !a.EmployerOriginalNameHeb.ToLower().Contains("test")
                    orderby a.Popularity ascending
                    select new DetailedEmployer
                    {
                        EmployerId = a.EmployerId,
                        EmployerName = a.EmployerOriginalNameHeb
                    }).Take(10).ToList();
        }
    }
}
