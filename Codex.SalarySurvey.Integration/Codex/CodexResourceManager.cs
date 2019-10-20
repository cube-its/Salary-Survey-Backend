using Codex.SalarySurvey.Common.Settings;
using Codex.SalarySurvey.Integration.Rest;
using Codex.SalarySurvey.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace Codex.SalarySurvey.Integration
{
    public class CodexResourceManager : ICodexResourceManager
    {
        private readonly string _baseApiUrl;
        private readonly AppSettings _appSettings;

        public CodexResourceManager(IOptions<AppSettings> appSettingsAccessor)
        {
            _appSettings = appSettingsAccessor.Value;
            _baseApiUrl = _appSettings.Integration.Codex.BaseApiUrl;
        }

        public bool PhoneExists(string phone)
        {
            try
            {
                string apiPath = $"visitors/availability/phone/{phone}";
                string result = new RESTClient().GetRequest(_baseApiUrl + apiPath);
                return Convert.ToBoolean(JsonConvert.DeserializeObject<PhoneAvailability>(result).Data);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string SendSmsVerificationCode(string phone)
        {
            string apiPath = $"utils/phone/sms/{phone}/4";
            string result = new RESTClient().PostRequest(_baseApiUrl + apiPath);
            return JsonConvert.DeserializeObject<SmsVerificationCode>(result).Code;
        }
    }
}
