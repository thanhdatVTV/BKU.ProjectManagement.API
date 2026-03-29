using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppStudent : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid SsoStudentId { get; set; }
        public string StudentCode { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int MajorId { get; set; }
        public Guid? ClassGroupId { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Status { get; set; } = 1;
        public DateTime LastSyncedAt { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual AppMajor Major { get; set; } = null!;
        public virtual AppClassGroup? ClassGroup { get; set; }
        public virtual AppFaculty? Faculty { get; set; }
        public virtual AppCourse? Course { get; set; }

        public virtual ICollection<StudentProjectRegistration> StudentProjectRegistrations { get; set; } = new List<StudentProjectRegistration>();
        public virtual ICollection<ProjectTeam> LeadProjectTeams { get; set; } = new List<ProjectTeam>();
        public virtual ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; } = new List<ProjectTeamMember>();
        public virtual ICollection<ProgressReport> CreatedProgressReports { get; set; } = new List<ProgressReport>();
    }
}
