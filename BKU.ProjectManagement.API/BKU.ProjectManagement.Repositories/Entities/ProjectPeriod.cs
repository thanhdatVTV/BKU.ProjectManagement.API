using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class ProjectPeriod : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid SemesterId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string AcademicYear { get; set; } = null!;
        public int Stage { get; set; }
        public int Type { get; set; } // Using int to store ProjectPeriodType
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; } = 1;

        public virtual Semester Semester { get; set; } = null!;
        public virtual ICollection<LecturerCapacity> LecturerCapacities { get; set; } = new List<LecturerCapacity>();
        public virtual ICollection<StudentProjectRegistration> StudentProjectRegistrations { get; set; } = new List<StudentProjectRegistration>();
        public virtual ICollection<ProjectTeam> ProjectTeams { get; set; } = new List<ProjectTeam>();
        public virtual ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public virtual ICollection<TrainingOfficeResult> TrainingOfficeResults { get; set; } = new List<TrainingOfficeResult>();
    }
}
