using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblTeacher : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string TeacherId { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public int CourseId { get; set; }

        public virtual TblCourse Course { get; set; } = null!;
        public virtual TblUser User { get; set; } = null!;
    }
}
