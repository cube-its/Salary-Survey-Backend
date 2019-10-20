using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// This API controller contains APIs for survey.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/survey")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyService surveyService, ISurveyQuestionService surveyQuestionService, ISecurityService securityService, IMapper mapper)
        {
            _surveyService = surveyService;
            _surveyQuestionService = surveyQuestionService;
            _securityService = securityService;
            _mapper = mapper;
        }

        // POST: api/survey/start
        /// <summary>
        /// Starts the survey form.
        /// </summary>
        /// <returns>Survey form questions with the last saved page</returns>
        [HttpPost("start")]
        public ActionResult StartSurvey()
        {
            User loggedInUser = _securityService.GetAuthenticatedUser();

            var survey = _surveyService.GetDefaultSurvey();

            if (survey == null)
                throw new NotFoundException();

            JoiningPlatform platform = Request.Headers["User-Agent"].Contains("Mobile") ? JoiningPlatform.Mobile : JoiningPlatform.Desktop;

            QuestionsSurvey surveyData = _surveyService.StartSurvey(loggedInUser.Id, survey.Id, platform);
            GetSurveyDTO surveyDTO = _mapper.Map<GetSurveyDTO>(surveyData);

            return Ok(surveyDTO);
        }

        // POST: api/survey/answers
        /// <summary>
        /// Saves the answers of the survey questions.
        /// </summary>
        /// <param name="saveSurveyDTO">Survey answers</param>
        /// <returns>Feedback status</returns>
        [HttpPost("answers")]
        public ActionResult SaveSurveyAnswers([FromBody]SaveSurveyDTO saveSurveyDTO)
        {
            User loggedInUser = _securityService.GetAuthenticatedUser();

            var answers = _mapper.Map<List<QuestionAnswer>>(saveSurveyDTO.Questions);

            var survey = _surveyService.GetDefaultSurvey();
            _surveyService.SaveSurveyAnswers(loggedInUser.Id, survey, answers, saveSurveyDTO.PageNum);

            return Ok();
        }

        // GET: api/survey/reset/phone/{phone}
        /// <summary>
        /// Resets the answers of the survey questions.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>Feedback status</returns>
        [AllowAnonymous]
        [HttpGet("reset/phone/{phone}")]
        public ActionResult ResetSurveyAnswers([Phone] string phone)
        {
            var survey = _surveyService.GetDefaultSurvey();
            _surveyService.ResetSurveyAnswers(phone, survey.Id);

            return Ok();
        }
    }
}
