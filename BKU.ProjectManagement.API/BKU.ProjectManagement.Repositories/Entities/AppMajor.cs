using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppMajor : BaseEntity
    {
        public int Id { get; set; }
        public int SsoMajorId { get; set; }
        public int FacultyId { get; set; }
        public string MajorName { get; set; } = null!;
        public DateTime LastSyncedAt { get; set; }

        public virtual AppFaculty Faculty { get; set; } = null!;
        public virtual ICollection<AppStudent> AppStudents { get; set; } = new List<AppStudent>();
        public virtual ICollection<AppLecturer> AppLecturers { get; set; } = new List<AppLecturer>();
        public virtual ICollection<LecturerCapacity> LecturerCapacities { get; set; } = new List<LecturerCapacity>();
        public virtual ICollection<StudentProjectRegistration> StudentProjectRegistrations { get; set; } = new List<StudentProjectRegistration>();
    }
}
