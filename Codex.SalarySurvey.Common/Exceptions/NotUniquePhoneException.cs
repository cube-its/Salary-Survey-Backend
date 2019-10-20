using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Common.Exceptions
{
    public class NotUniquePhoneException : Exception
    {
        private const string Message = "Phone number is already registered!";

        public NotUniquePhoneException()
            : base(Message)
        {
        }
    }
}
