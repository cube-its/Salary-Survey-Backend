﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Model
{
    public class PhoneAvailability
    {
        public bool Success { get; set; }
        public int Data { get; set; }
    }

    public class SmsVerificationCode
    {
        public string Code { get; set; }
    }
}
