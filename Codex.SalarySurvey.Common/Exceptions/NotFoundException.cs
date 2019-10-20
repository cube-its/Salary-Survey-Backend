using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        private const string Message = "Not Found";

        public NotFoundException()
            : base(Message)
        {
        }
    }
}
