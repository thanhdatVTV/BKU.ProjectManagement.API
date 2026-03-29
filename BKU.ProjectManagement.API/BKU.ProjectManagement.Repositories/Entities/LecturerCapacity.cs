using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class LecturerCapacity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public Guid LecturerId { get; set; }
        public int? MajorId { get; set; }
        public int MaxStudents { get; set; }
        public int? MaxTeams { get; set; }
        public int CurrentStudents { get; set; } = 0;
        public int CurrentTeams { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public string? Note { get; set; }

        public virtual ProjectPeriod ProjectPeriod { get; set; } = null!;
        public virtual AppLecturer Lecturer { get; set; } = null!;
        public virtual AppMajor? Major { get; set; }
    }
}
