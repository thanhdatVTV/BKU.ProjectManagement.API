using BKU.ProjectManagement.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Configs
{
    public class FinalSubmissionConfiguration : IEntityTypeConfiguration<FinalSubmission>
    {
        public void Configure(EntityTypeBuilder<FinalSubmission> builder)
        {
            builder.ToTable("FinalSubmission");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VersionNo).HasDefaultValue(1).IsRequired();
            builder.Property(x => x.ReportTitle).HasMaxLength(500).IsRequired();
            builder.Property(x => x.SourceCodeUrl).HasMaxLength(1000);
            builder.Property(x => x.DemoUrl).HasMaxLength(1000);
            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.ReviewComment).HasMaxLength(2000);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => new { x.ProjectTeamId, x.VersionNo }).IsUnique();

            builder.HasOne(x => x.ProjectTeam)
                .WithMany(x => x.FinalSubmissions)
                .HasForeignKey(x => x.ProjectTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProjectTopic)
                .WithMany(x => x.FinalSubmissions)
                .HasForeignKey(x => x.ProjectTopicId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReviewedByUser)
                .WithMany(x => x.ReviewedFinalSubmissions)
                .HasForeignKey(x => x.ReviewedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
