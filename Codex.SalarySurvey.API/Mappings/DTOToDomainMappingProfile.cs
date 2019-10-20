using AutoMapper;
using Codex.SalarySurvey.API.DTOs;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codex.SalarySurvey.API.Mappings
{
    public class DTOToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DTOToDomainMappings"; }
        }

        public DTOToDomainMappingProfile()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<QuestionAnswerPostDTO, QuestionAnswer>();
        }
    }
}
