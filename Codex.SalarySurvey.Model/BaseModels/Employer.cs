using System;
using System.Collections.Generic;

namespace Codex.SalarySurvey.Model
{
    public partial class Employer
    {
        public int Id { get; set; }
        public int? EmployerId { get; set; }
        public string EmployerName { get; set; }
        public bool? EmployerActive { get; set; }
        public string EmployerOriginalNameHeb { get; set; }
        public int? Popularity { get; set; }
    }
}
