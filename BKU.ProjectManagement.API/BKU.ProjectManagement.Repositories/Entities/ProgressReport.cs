using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class ProgressReport : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid ProjectTopicId { get; set; }
        public int ReportNo { get; set; }
        public int? WeekNo { get; set; }
        public string Title { get; set; } = null!;
        public string? Summary { get; set; }
        public string? CompletedWork { get; set; }
        public string? PlannedWork { get; set; }
        public string? Issues { get; set; }
        public decimal PercentComplete { get; set; } = 0;
        public int Status { get; set; } = 0;
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public Guid? ReviewedByTeacherId { get; set; }
        public string? TeacherComment { get; set; }
        public Guid CreatedByStudentId { get; set; }

        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual ProjectTopic ProjectTopic { get; set; } = null!;
        public virtual AppLecturer? ReviewedByTeacher { get; set; }
        public virtual AppStudent CreatedByStudent { get; set; } = null!;

        public virtual ICollection<ProgressReportAttachment> ProgressReportAttachments { get; set; } = new List<ProgressReportAttachment>();
    }
}
