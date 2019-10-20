using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codex.SalarySurvey.API.DTOs
{
    public class QuestionGetDTO
    {
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

        public IEnumerable<QuestionOptionGetDTO> Options { get; set; }
    }

    public class QuestionOptionGetDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool AutocompleteOnly { get; set; }
        public int SortOrder { get; set; }
    }
}
