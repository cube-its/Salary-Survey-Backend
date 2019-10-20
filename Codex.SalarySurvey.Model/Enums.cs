using System;

namespace Codex.SalarySurvey.Model
{
    public enum QuestionType
    {
        OpenText = 1,
        Select = 2,
        MultiSelect = 3,
        MultiTags = 4,
        Rank = 5,
        MultiRank = 6,
        Slider = 7
    }

    public enum SurveyType
    {
        Legal = 1
    }

    public enum JoiningPlatform
    {
        Desktop = 1,
        Mobile = 2
    }

    public enum EntityStatus
    {
        InActive = 0,
        Active = 1,
    }

    public enum ErrorCode
    {
        NotFound = 404,
        BadRequest = 400,
        InternalServerError = 500,
        NotUniqueEmail = 10001,
        NotUniquePhone = 10002,
        InvalidPhoneOrEmail = 10003
    }
}
