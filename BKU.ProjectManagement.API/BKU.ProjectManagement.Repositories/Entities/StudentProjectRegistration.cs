using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class StudentProjectRegistration : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public Guid StudentId { get; set; }
        public int SelectedMajorId { get; set; }
        public int Status { get; set; }
        public Guid? ApprovedLecturerId { get; set; }
        public string? RejectReason { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public Guid? ReviewedByUserId { get; set; }

        public virtual ProjectPeriod ProjectPeriod { get; set; } = null!;
        public virtual AppStudent Student { get; set; } = null!;
        public virtual AppMajor SelectedMajor { get; set; } = null!;
        public virtual AppLecturer? ApprovedLecturer { get; set; }
        public virtual AppUser? ReviewedByUser { get; set; }

        public virtual ICollection<StudentProjectRegistrationChoice> StudentProjectRegistrationChoices { get; set; } = new List<StudentProjectRegistrationChoice>();
        public virtual ICollection<RegistrationReviewHistory> RegistrationReviewHistories { get; set; } = new List<RegistrationReviewHistory>();
    }
}
