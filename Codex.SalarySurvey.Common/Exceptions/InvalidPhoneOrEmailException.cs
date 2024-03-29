﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Common.Exceptions
{
    public class InvalidPhoneOrEmailException : Exception
    {
        private const string Message = "Invalid phone or email!";

        public InvalidPhoneOrEmailException()
            : base(Message)
        {
        }
    }
}
