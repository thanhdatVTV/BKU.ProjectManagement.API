using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblFaculty : BaseEntity
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string FacultyName { get; set; } = null!;

        public virtual TblCourse Course { get; set; } = null!;
        public virtual ICollection<TblMajor> TblMajors { get; set; } = new List<TblMajor>();
    }
}
