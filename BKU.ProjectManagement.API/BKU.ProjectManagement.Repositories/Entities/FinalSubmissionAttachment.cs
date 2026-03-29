using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class FinalSubmissionAttachment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid FinalSubmissionId { get; set; }
        public int FileCategory { get; set; }
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public string? FileType { get; set; }
        public long? FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
        public Guid UploadedBy { get; set; }

        public virtual FinalSubmission FinalSubmission { get; set; } = null!;
        public virtual AppUser UploadedByUser { get; set; } = null!;
    }
}
