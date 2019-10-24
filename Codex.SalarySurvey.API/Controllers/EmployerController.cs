using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Codex.SalarySurvey.API.DTOs;
using Codex.SalarySurvey.Domain.Contracts.Services;
using Codex.SalarySurvey.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codex.SalaryEmployer.API.Controllers
{
    /// <summary>
    /// This API controller contains APIs for employers.
    /// </summary>
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/employer")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;
        private readonly IMapper _mapper;

        public EmployerController(IEmployerService employerService, IMapper mapper)
        {
            _employerService = employerService;
            _mapper = mapper;
        }

        // GET: api/employer
        /// <summary>
        /// Gets all employers.
        /// </summary>
        /// <returns>List of employers</returns>
        [HttpGet]
        public ActionResult<IEnumerable<EmployerGetDTO>> GetEmployers([FromQuery]string filter = null)
        {
            IEnumerable<DetailedEmployer> employers = _employerService.GetEmployers(filter);
            IEnumerable<EmployerGetDTO> employersDTO = _mapper.Map<IEnumerable<EmployerGetDTO>>(employers);

            return Ok(employersDTO);
        }
    }
}
