using Codex.SalarySurvey.Data.Infrastructure;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codex.SalarySurvey.Data.Repositories
{
    public class QuestionAnswerRepository : RepositoryBase<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public List<QuestionAnswer> GetSurveyAnswers(int userId, int surveyId)
        {
            return DbContext.QuestionAnswers
                .Where(a => a.UserId == userId && a.SurveyQuestion != null && a.SurveyQuestion.SurveyId == surveyId).ToList();
        }

        public void AddList(List<QuestionAnswer> answers)
        {
            DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            DbContext.QuestionAnswers.AddRange(answers);
        }
    }
}
