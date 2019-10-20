using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class Survey
    {
        public Survey()
        {
            SurveyQuestions = new HashSet<SurveyQuestion>();
            UserSurveys = new HashSet<UserSurvey>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public DateTime? StartsOn { get; set; }
        public DateTime? EndsOn { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
        public virtual ICollection<UserSurvey> UserSurveys { get; set; }
    }
}
