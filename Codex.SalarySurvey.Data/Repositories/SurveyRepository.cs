using Codex.SalarySurvey.Data.Infrastructure;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codex.SalarySurvey.Data.Repositories
{
    public class SurveyRepository : RepositoryBase<Survey>, ISurveyRepository
    {
        public SurveyRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public DetailedSurvey GetDefaultSurvey()
        {
            return (from a in DbContext.Surveys
                    select new DetailedSurvey
                    {
                        Id = a.Id,
                        Name = a.Name,
                        NumOfPages = a.SurveyQuestions.Max(m => m.Page),
                        Type = (SurveyType)a.Type,
                        StartsOn = a.StartsOn,
                        EndsOn = a.EndsOn,
                        Status = (EntityStatus)a.Status,
                        CreatedOn = a.CreatedOn
                    }).FirstOrDefault();
        }

        public UserSurvey GetUserSurvey(int userId, int surveyId)
        {
            return DbContext.UserSurveys
                .FirstOrDefault(a => a.UserId == userId && a.SurveyId == surveyId);
        }

        public void AddUserSurvey(UserSurvey userSurvey)
        {
            DbContext.UserSurveys.Add(userSurvey);
        }
    }
}
