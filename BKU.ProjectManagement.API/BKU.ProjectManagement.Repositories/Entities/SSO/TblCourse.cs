using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblCourse : BaseEntity
    {
        public int Id { get; set; }
        public string CourseName { get; set; } = null!;

        public virtual ICollection<TblFaculty> TblFaculties { get; set; } = new List<TblFaculty>();
        public virtual ICollection<TblTeacher> TblTeachers { get; set; } = new List<TblTeacher>();
    }
}
