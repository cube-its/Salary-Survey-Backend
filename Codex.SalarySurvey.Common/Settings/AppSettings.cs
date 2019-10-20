using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.SalarySurvey.Common.Settings
{
    /// <summary>
    /// Class that holds the application settings.
    /// </summary>
    public class AppSettings
    {
        public IntegrationSettings Integration { get; set; }
        public int SmsCodeExpiryInMinutes { get; set; }
    }

    public class IntegrationSettings
    {
        public CodexIntegrationSettings Codex { get; set; }
    }

    public class CodexIntegrationSettings
    {
        public string BaseApiUrl { get; set; }
    }
}
