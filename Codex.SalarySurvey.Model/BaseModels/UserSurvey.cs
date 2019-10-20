using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class UserSurvey
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SurveyId { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int PageCompleted { get; set; }
        public int JoiningPlatform { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual User User { get; set; }
    }
}
