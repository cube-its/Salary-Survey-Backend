using Codex.SalarySurvey.Data.Infrastructure;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Codex.SalarySurvey.Data.Repositories
{
    public class SurveyQuestionRepository : RepositoryBase<Question>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public IEnumerable<DetailedQuestion> GetSurveyQuestions(int surveyId, int? userId = null)
        {
            var lst = from a in DbContext.Questions
                      join b in DbContext.SurveyQuestions
                      on a.Id equals b.QuestionId
                      where b.SurveyId == surveyId
                      orderby b.Page ascending, b.SortOrder ascending
                      select new DetailedQuestion
                      {
                          Id = a.Id,
                          SurveyQuestionId = b.Id,
                          Text = a.Text,
                          Placeholder = a.Placeholder,
                          ToolTip = a.ToolTip,
                          Answer = b.QuestionAnswers.FirstOrDefault(c => c.UserId == userId).Answer,
                          Type = (QuestionType)a.Type,
                          Params = a.Params,
                          IsRequired = a.IsRequired,
                          RegEx = a.RegEx,
                          Page = b.Page,
                          SortOrder = b.SortOrder,
                          Options = a.QuestionOptions.OrderBy(o => o.SortOrder).Select(o => new DetailedQuestionOption
                          {
                              Id = o.Id,
                              Text = o.Text,
                              AutocompleteOnly = o.AutocompleteOnly,
                              SortOrder = o.SortOrder
                          })
                      };

            return lst.ToList();
        }
    }
}
