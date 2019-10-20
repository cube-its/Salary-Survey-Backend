using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Domain.Contracts.Repositories
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        /// <summary>
        /// Gets default survey.
        /// </summary>
        /// <returns>Survey object</returns>
        DetailedSurvey GetDefaultSurvey();

        /// <summary>
        /// Gets user survey record.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="surveyId">Survey ID</param>
        /// <returns>User survey record</returns>
        UserSurvey GetUserSurvey(int userId, int surveyId);

        /// <summary>
        /// Adds user survey record.
        /// </summary>
        /// <param name="userSurvey">User survey object</param>
        void AddUserSurvey(UserSurvey userSurvey);
    }
}
