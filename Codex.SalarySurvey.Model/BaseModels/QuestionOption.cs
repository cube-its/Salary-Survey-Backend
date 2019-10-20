using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class QuestionOption
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public int SortOrder { get; set; }
        public bool AutocompleteOnly { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Question Question { get; set; }
    }
}
