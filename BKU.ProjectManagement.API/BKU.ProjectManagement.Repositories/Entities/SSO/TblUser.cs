using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblUser : BaseEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Type { get; set; }

        public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
        public virtual ICollection<TblTeacher> TblTeachers { get; set; } = new List<TblTeacher>();
    }
}
