using Codex.SalarySurvey.Domain.Contracts;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Domain.Contracts.Services;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codex.SalarySurvey.Domain
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployerService(IEmployerRepository employerRepository, IUnitOfWork unitOfWork)
        {
            _employerRepository = employerRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<DetailedEmployer> GetEmployers(string filter = null)
        {
            return _employerRepository.GetEmployers(filter);
        }
    }
}
