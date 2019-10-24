using AutoMapper;
using Codex.SalarySurvey.API.DTOs;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codex.SalarySurvey.API.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToDTOMappings"; }
        }

        public DomainToDTOMappingProfile()
        {
            CreateMap<DetailedEmployer, EmployerGetDTO>();
            CreateMap<User, UserLoginResultDTO>();
            CreateMap<User, UserRegisterResultDTO>();
            CreateMap<QuestionsSurvey, GetSurveyDTO>();
            CreateMap<DetailedQuestion, QuestionGetDTO>();
            CreateMap<DetailedQuestionOption, QuestionOptionGetDTO>();
        }
    }
}
