using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Repositories
{
    public interface ISurveyQuestionRepository : IRepository<Question>
    {
        /// <summary>
        /// Gets all survey questions.
        /// </summary>
        /// <param name="surveyId">Survey ID</param>
        /// <returns>List of questions</returns>
        IEnumerable<DetailedQuestion> GetSurveyQuestions(int surveyId, int? userId);
    }
}
