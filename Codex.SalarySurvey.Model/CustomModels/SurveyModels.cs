using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Model
{
    public class DetailedSurvey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfPages { get; set; }
        public SurveyType Type { get; set; }
        public DateTime? StartsOn { get; set; }
        public DateTime? EndsOn { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class QuestionsSurvey
    {
        public QuestionsSurvey()
        {
            Questions = new List<DetailedQuestion>();
        }

        public int PageNum { get; set; }
        public IEnumerable<DetailedQuestion> Questions { get; set; }
    }
}
