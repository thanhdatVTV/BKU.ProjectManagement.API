using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppFaculty : BaseEntity
    {
        public int Id { get; set; }
        public int SsoFacultyId { get; set; }
        public int? CourseId { get; set; }
        public string FacultyName { get; set; } = null!;
        public DateTime LastSyncedAt { get; set; }

        public virtual AppCourse? Course { get; set; }
        public virtual ICollection<AppMajor> AppMajors { get; set; } = new List<AppMajor>();
        public virtual ICollection<AppStudent> AppStudents { get; set; } = new List<AppStudent>();
        public virtual ICollection<AppLecturer> AppLecturers { get; set; } = new List<AppLecturer>();
    }
}
