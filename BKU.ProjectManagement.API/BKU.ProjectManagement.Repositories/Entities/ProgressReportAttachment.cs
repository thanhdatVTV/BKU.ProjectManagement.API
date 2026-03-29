using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class ProgressReportAttachment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProgressReportId { get; set; }
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public string? FileType { get; set; }
        public long? FileSize { get; set; }
        public Guid UploadedBy { get; set; }
        public DateTime UploadedAt { get; set; }

        public virtual ProgressReport ProgressReport { get; set; } = null!;
        public virtual AppUser UploadedByUser { get; set; } = null!;
    }
}
