using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppUser : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid SsoUserId { get; set; }
        public string UserName { get; set; } = null!;
        public int UserType { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int SyncStatus { get; set; } = 1;

        public virtual AppStudent? AppStudent { get; set; }
        public virtual AppLecturer? AppLecturer { get; set; }

        public virtual ICollection<StudentProjectRegistration> ReviewedStudentProjectRegistrations { get; set; } = new List<StudentProjectRegistration>();
        public virtual ICollection<RegistrationReviewHistory> RegistrationReviewHistories { get; set; } = new List<RegistrationReviewHistory>();
        public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public virtual ICollection<ProjectTopic> ApprovedProjectTopics { get; set; } = new List<ProjectTopic>();
        public virtual ICollection<ProgressReportAttachment> ProgressReportAttachments { get; set; } = new List<ProgressReportAttachment>();
        public virtual ICollection<FinalSubmission> ReviewedFinalSubmissions { get; set; } = new List<FinalSubmission>();
        public virtual ICollection<FinalSubmissionAttachment> FinalSubmissionAttachments { get; set; } = new List<FinalSubmissionAttachment>();
        public virtual ICollection<TrainingOfficeResult> SentTrainingOfficeResults { get; set; } = new List<TrainingOfficeResult>();
    }
}
