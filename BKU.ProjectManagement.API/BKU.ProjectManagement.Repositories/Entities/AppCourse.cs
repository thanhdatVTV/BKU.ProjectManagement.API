using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppCourse : BaseEntity
    {
        public int Id { get; set; }
        public int SsoCourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public DateTime LastSyncedAt { get; set; }

        public virtual ICollection<AppFaculty> AppFaculties { get; set; } = new List<AppFaculty>();
        public virtual ICollection<AppStudent> AppStudents { get; set; } = new List<AppStudent>();
        public virtual ICollection<AppLecturer> AppLecturers { get; set; } = new List<AppLecturer>();
    }
}
