using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities.SSO
{
    public class TblClassGroup : BaseEntity
    {
        public Guid Id { get; set; }
        public string ClassGroupName { get; set; } = null!;

        public virtual ICollection<TblStudent> TblStudents { get; set; } = new List<TblStudent>();
    }
}
