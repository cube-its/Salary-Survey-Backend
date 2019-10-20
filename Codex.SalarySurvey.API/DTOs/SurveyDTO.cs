using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codex.SalarySurvey.API.DTOs
{
    public class SaveSurveyDTO
    {
        [Required]
        [Range(1, 5)]
        public int PageNum { get; set; }
        [Required]
        public List<QuestionAnswerPostDTO> Questions { get; set; }
    }

    public class GetSurveyDTO
    {
        public int PageNum { get; set; }
        public List<QuestionGetDTO> Questions { get; set; }
    }
}
