using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class TeacherAssignment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid TeacherId { get; set; }
        public DateTime AssignedAt { get; set; }
        public Guid AssignedBy { get; set; }
        public int Status { get; set; } = 1;
        public string? Note { get; set; }

        public virtual ProjectPeriod ProjectPeriod { get; set; } = null!;
        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual AppLecturer Teacher { get; set; } = null!;
        public virtual AppUser AssignedByUser { get; set; } = null!;
    }
}
