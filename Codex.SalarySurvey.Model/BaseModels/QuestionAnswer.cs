using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class QuestionAnswer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SurveyQuestionId { get; set; }
        public string Answer { get; set; }
        public int? EmployerId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public virtual User User { get; set; }
    }
}
