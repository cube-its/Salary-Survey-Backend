using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class User
    {
        public User()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
            UserSurveys = new HashSet<UserSurvey>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SmsCode { get; set; }
        public DateTime? SmsCodeExpiredOn { get; set; }
        public DateTime? SmsCodePassedOn { get; set; }
        public bool IsCodexUser { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<UserSurvey> UserSurveys { get; set; }
    }
}
