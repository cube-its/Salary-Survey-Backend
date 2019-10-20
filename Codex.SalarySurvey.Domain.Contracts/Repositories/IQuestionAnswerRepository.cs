using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Repositories
{
    public interface IQuestionAnswerRepository : IRepository<QuestionAnswer>
    {
        /// <summary>
        /// Gets survey answers.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="surveyId">Survey ID</param>
        /// <returns>Survey answers</returns>
        List<QuestionAnswer> GetSurveyAnswers(int userId, int surveyId);

        /// <summary>
        /// Adds list of question answers to the database context.
        /// </summary>
        /// <param name="answers">Question answers</param>
        void AddList(List<QuestionAnswer> answers);
    }
}
