using Codex.SalarySurvey.Common.Settings;
using System;

namespace Codex.SalarySurvey.Integration
{
    /// <summary>
    /// Interface that defines the integration with Codex system resources.
    /// </summary>
    public interface ICodexResourceManager
    {
        /// <summary>
        /// Checks if the given phone number already exists in Codex database.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>Boolean</returns>
        bool PhoneExists(string phone);

        /// <summary>
        /// Sends SMS verification code to the given phone number.
        /// </summary>
        /// <param name="phone">Phone number</param>
        /// <returns>Verification code</returns>
        string SendSmsVerificationCode(string phone);
    }
}
