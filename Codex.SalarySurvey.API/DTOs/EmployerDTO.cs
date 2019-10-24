using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Codex.SalarySurvey.API.DTOs
{
    public class EmployerGetDTO
    {
        public int? EmployerId { get; set; }
        public string EmployerName { get; set; }
    }
}
