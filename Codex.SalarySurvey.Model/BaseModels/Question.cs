using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class Question
    {
        public Question()
        {
            QuestionOptions = new HashSet<QuestionOption>();
            SurveyQuestions = new HashSet<SurveyQuestion>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public string Placeholder { get; set; }
        public string ToolTip { get; set; }
        public int Type { get; set; }
        public bool IsRequired { get; set; }
        public string RegEx { get; set; }
        public string Params { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
