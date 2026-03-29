using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class ProjectTeam : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public string TeamCode { get; set; } = null!;
        public string? TeamName { get; set; }
        public Guid? LeaderStudentId { get; set; }
        public Guid? AssignedLecturerId { get; set; }
        public int Status { get; set; } = 0;

        public virtual ProjectPeriod ProjectPeriod { get; set; } = null!;
        public virtual AppStudent? LeaderStudent { get; set; }
        public virtual AppLecturer? AssignedLecturer { get; set; }

        public virtual ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; } = new List<ProjectTeamMember>();
        public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public virtual ICollection<ProjectTopic> ProjectTopics { get; set; } = new List<ProjectTopic>();
        public virtual ICollection<ProgressReport> ProgressReports { get; set; } = new List<ProgressReport>();
        public virtual ICollection<FinalSubmission> FinalSubmissions { get; set; } = new List<FinalSubmission>();
        public virtual ICollection<TrainingOfficeResult> TrainingOfficeResults { get; set; } = new List<TrainingOfficeResult>();
    }
}
