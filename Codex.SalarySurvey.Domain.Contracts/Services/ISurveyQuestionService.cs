using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Services
{
    /// <summary>
    /// Survey question service interface that exposes the relevant operations.
    /// </summary>
    public interface ISurveyQuestionService
    {
        /// <summary>
        /// Gets all survey questions.
        /// </summary>
        /// <param name="surveyId">Survey ID</param>
        /// <returns>List of questions</returns>
        IEnumerable<DetailedQuestion> GetSurveyQuestions(int surveyId, int? userId);
    }
}
