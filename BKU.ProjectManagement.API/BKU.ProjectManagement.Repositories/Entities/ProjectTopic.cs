using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class ProjectTopic : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? Objectives { get; set; }
        public string? TechnologyStack { get; set; }
        public string? ScopeIn { get; set; }
        public string? ScopeOut { get; set; }
        public string? DomainType { get; set; }
        public int Status { get; set; } = 0;
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public Guid? ApprovedBy { get; set; }
        public string? RejectReason { get; set; }

        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual AppLecturer Teacher { get; set; } = null!;
        public virtual AppUser? ApprovedByUser { get; set; }

        public virtual ICollection<ProgressReport> ProgressReports { get; set; } = new List<ProgressReport>();
        public virtual ICollection<FinalSubmission> FinalSubmissions { get; set; } = new List<FinalSubmission>();
    }
}
