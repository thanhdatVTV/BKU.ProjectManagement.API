using BKU.ProjectManagement.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Entities
{
    public class TrainingOfficeResult : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProjectPeriodId { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid TeacherId { get; set; }
        public decimal? FinalScore { get; set; }
        public int ResultStatus { get; set; } = 0;
        public string? EvaluationNote { get; set; }
        public DateTime? SentAt { get; set; }
        public Guid? SentBy { get; set; }
        public int ReceiveStatus { get; set; } = 0;
        public string? ExternalRefNo { get; set; }

        public virtual ProjectPeriod ProjectPeriod { get; set; } = null!;
        public virtual ProjectTeam ProjectTeam { get; set; } = null!;
        public virtual AppLecturer Teacher { get; set; } = null!;
        public virtual AppUser? SentByUser { get; set; }
    }
}
