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
        public int Role { get; set; }
        public DateTime JoinedAt { get; set; }
        public bool IsActiveMember { get; set; } = true;

        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual AppStudent Student { get; set; } = null!;
    }
}
