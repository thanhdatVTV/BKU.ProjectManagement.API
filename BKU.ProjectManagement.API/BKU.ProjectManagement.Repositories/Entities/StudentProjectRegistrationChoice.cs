using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class StudentProjectRegistrationChoice : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid RegistrationId { get; set; }
        public int PriorityOrder { get; set; }
        public Guid LecturerId { get; set; }
        public int Status { get; set; } = 0;
        public string? Note { get; set; }

        public virtual StudentProjectRegistration Registration { get; set; } = null!;
        public virtual AppLecturer Lecturer { get; set; } = null!;
    }
}
