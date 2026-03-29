using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class AppClassGroup : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid SsoClassGroupId { get; set; }
        public string ClassGroupName { get; set; } = null!;
        public DateTime LastSyncedAt { get; set; }

        public virtual ICollection<AppStudent> AppStudents { get; set; } = new List<AppStudent>();
    }
}
