using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class SurveyQuestion
    {
        public SurveyQuestion()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }
        public int Page { get; set; }
        public int SortOrder { get; set; }

        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
