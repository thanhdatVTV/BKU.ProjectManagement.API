using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class SsoSyncLog : BaseEntity
    {
        public Guid Id { get; set; }
        public string SyncType { get; set; } = null!;
        public string? SsoEntityId { get; set; }
        public Guid? AppEntityId { get; set; }
        public int Status { get; set; }
        public string? Message { get; set; }
        public DateTime SyncedAt { get; set; }
    }
}
