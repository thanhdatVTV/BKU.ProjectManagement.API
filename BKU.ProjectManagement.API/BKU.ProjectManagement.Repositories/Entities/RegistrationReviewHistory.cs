using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class RegistrationReviewHistory : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid RegistrationId { get; set; }
        public int Action { get; set; }
        public int? FromStatus { get; set; }
        public int? ToStatus { get; set; }
        public Guid ActionBy { get; set; }
        public DateTime ActionAt { get; set; }
        public string? Comment { get; set; }

        public virtual StudentProjectRegistration Registration { get; set; } = null!;
        public virtual AppUser ActionByUser { get; set; } = null!;
    }
}
