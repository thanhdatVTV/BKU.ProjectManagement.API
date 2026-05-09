using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class ProjectTeamMember : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid StudentId { get; set; }
        public int Role { get; set; } // 1: Leader, 2: Member
        public DateTime JoinedAt { get; set; }
        public bool IsActiveMember { get; set; } = true;
        public int Status { get; set; } = 0; // 0: Invited, 1: Accepted, 2: Declined, 3: Left
        public Guid? InvitedBy { get; set; }

        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual AppStudent Student { get; set; } = null!;
    }
}
