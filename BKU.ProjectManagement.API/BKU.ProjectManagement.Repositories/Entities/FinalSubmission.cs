using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class FinalSubmission : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid ProjectTopicId { get; set; }
        public int VersionNo { get; set; } = 1;
        public string ReportTitle { get; set; } = null!;
        public string? Abstract { get; set; }
        public string? SourceCodeUrl { get; set; }
        public string? DemoUrl { get; set; }
        public int Status { get; set; } = 0;
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public Guid? ReviewedBy { get; set; }
        public string? ReviewComment { get; set; }

        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual ProjectTopic ProjectTopic { get; set; } = null!;
        public virtual AppUser? ReviewedByUser { get; set; }

        public virtual ICollection<FinalSubmissionAttachment> FinalSubmissionAttachments { get; set; } = new List<FinalSubmissionAttachment>();
    }
}
