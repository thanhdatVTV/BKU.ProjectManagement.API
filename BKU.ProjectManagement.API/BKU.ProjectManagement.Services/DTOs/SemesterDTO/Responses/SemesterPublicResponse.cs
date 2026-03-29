using BKU.ProjectManagement.Repositories.Entities;
using BKU.ProjectManagement.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Services.DTOs.SemesterDTO.Responses
{
    public class SemesterPublicResponse : BaseResponses
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        //public virtual ICollection<ProjectPeriod> ProjectPeriods { get; set; }
    }
}
