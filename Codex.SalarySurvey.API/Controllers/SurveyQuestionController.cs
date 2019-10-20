using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Codex.SalarySurvey.API.DTOs;
using Codex.SalarySurvey.Common.Exceptions;
using Codex.SalarySurvey.Domain.Contracts.Services;
using Codex.SalarySurvey.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Codex.SalarySurvey.API.Controllers
{
    /// <summary>
    /// This API controller contains APIs for survey questions.
    /// </summary>
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/survey")]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public SurveyQuestionController(ISurveyService surveyService, ISurveyQuestionService surveyQuestionService, ISecurityService securityService, IMapper mapper)
        {
            _surveyService = surveyService;
            _surveyQuestionService = surveyQuestionService;
            _securityService = securityService;
            _mapper = mapper;
        }

        // GET: api/survey/questions
        /// <summary>
        /// Gets all survey questions.
        /// </summary>
        /// <returns>List of questions</returns>
        [HttpGet("questions")]
        public ActionResult<IEnumerable<QuestionGetDTO>> GetSurveyQuestions([FromQuery]int? userId = null)
        {
            var survey = _surveyService.GetDefaultSurvey();
            if (survey == null)
            {
                throw new NotFoundException();
            }

            IEnumerable<DetailedQuestion> questions = _surveyQuestionService.GetSurveyQuestions(survey.Id, userId);
            IEnumerable<QuestionGetDTO> questionsDTO = _mapper.Map<IEnumerable<QuestionGetDTO>>(questions);

            return Ok(questionsDTO);
        }
    }
}
