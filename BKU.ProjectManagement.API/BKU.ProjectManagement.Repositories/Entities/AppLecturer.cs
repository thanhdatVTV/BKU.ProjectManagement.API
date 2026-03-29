using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppLecturer : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public Guid SsoTeacherId { get; set; }
        public string TeacherCode { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int? CourseId { get; set; }
        public int? FacultyId { get; set; }
        public int? MajorId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? MaxGroupsDefault { get; set; }
        public int Status { get; set; } = 1;
        public DateTime LastSyncedAt { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual AppCourse? Course { get; set; }
        public virtual AppFaculty? Faculty { get; set; }
        public virtual AppMajor? Major { get; set; }

        public virtual ICollection<LecturerCapacity> LecturerCapacities { get; set; } = new List<LecturerCapacity>();
        public virtual ICollection<StudentProjectRegistration> ApprovedStudentProjectRegistrations { get; set; } = new List<StudentProjectRegistration>();
        public virtual ICollection<StudentProjectRegistrationChoice> StudentProjectRegistrationChoices { get; set; } = new List<StudentProjectRegistrationChoice>();
        public virtual ICollection<ProjectTeam> AssignedProjectTeams { get; set; } = new List<ProjectTeam>();
        public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public virtual ICollection<ProjectTopic> ProjectTopics { get; set; } = new List<ProjectTopic>();
        public virtual ICollection<ProgressReport> ReviewedProgressReports { get; set; } = new List<ProgressReport>();
        public virtual ICollection<TrainingOfficeResult> TrainingOfficeResults { get; set; } = new List<TrainingOfficeResult>();
    }
}
