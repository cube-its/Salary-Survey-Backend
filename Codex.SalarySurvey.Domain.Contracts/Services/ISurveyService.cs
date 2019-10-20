using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Services
{
    /// <summary>
    /// Survey service interface that exposes the relevant operations.
    /// </summary>
    public interface ISurveyService
    {
        /// <summary>
        /// Gets default survey.
        /// </summary>
        /// <returns>Survey object</returns>
        DetailedSurvey GetDefaultSurvey();

        /// <summary>
        /// Starts the survey form.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="platform">Joining platform</param>
        /// <returns>Survey questions</returns>
        QuestionsSurvey StartSurvey(int userId, int surveyId, JoiningPlatform platform);

        /// <summary>
        /// Saves the answers of the survey questions.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="survey">Survey object</param>
        /// <param name="answers">Survey answers</param>
        /// <param name="pageNum">Page number</param>
        void SaveSurveyAnswers(int userId, DetailedSurvey survey, List<QuestionAnswer> answers, int pageNum);

        /// <summary>
        /// Resets the answers of the survey questions.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <param name="surveyId">Survey ID</param>
        void ResetSurveyAnswers(string phone, int surveyId);
    }
}
