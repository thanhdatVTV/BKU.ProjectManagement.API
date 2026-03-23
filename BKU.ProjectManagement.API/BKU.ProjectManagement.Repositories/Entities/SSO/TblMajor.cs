using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblMajor : BaseEntity
    {
        public int Id { get; set; }
        public int FacultyId { get; set; }
        public string MajorName { get; set; } = null!;

        public virtual TblFaculty Faculty { get; set; } = null!;
        public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
    }
}
