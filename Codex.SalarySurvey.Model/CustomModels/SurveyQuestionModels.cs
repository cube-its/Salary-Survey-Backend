using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Model
{
    public class DetailedQuestion
    {
        public DetailedQuestion()
        {
            Options = new List<DetailedQuestionOption>();
        }

        public int Id { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Text { get; set; }
        public string Placeholder { get; set; }
        public string ToolTip { get; set; }
        public string Answer { get; set; }
        public QuestionType Type { get; set; }
        public bool IsRequired { get; set; }
        public string RegEx { get; set; }
        public string Params { get; set; }
        public int Page { get; set; }
        public int SortOrder { get; set; }

        public IEnumerable<DetailedQuestionOption> Options { get; set; }
    }

    public class DetailedQuestionOption
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool AutocompleteOnly { get; set; }
        public int SortOrder { get; set; }
    }
}
