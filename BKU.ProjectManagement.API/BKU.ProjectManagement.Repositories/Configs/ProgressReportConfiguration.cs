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
    public class ProgressReportConfiguration : IEntityTypeConfiguration<ProgressReport>
    {
        public void Configure(EntityTypeBuilder<ProgressReport> builder)
        {
            builder.ToTable("ProgressReport");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(255).IsRequired();
            builder.Property(x => x.PercentComplete).HasColumnType("decimal(5,2)").HasDefaultValue(0);
            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.TeacherComment).HasMaxLength(2000);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => new { x.ProjectTeamId, x.ReportNo }).IsUnique();

            builder.HasOne(x => x.ProjectTeam)
                .WithMany(x => x.ProgressReports)
                .HasForeignKey(x => x.ProjectTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProjectTopic)
                .WithMany(x => x.ProgressReports)
                .HasForeignKey(x => x.ProjectTopicId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReviewedByTeacher)
                .WithMany(x => x.ReviewedProgressReports)
                .HasForeignKey(x => x.ReviewedByTeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CreatedByStudent)
                .WithMany(x => x.CreatedProgressReports)
                .HasForeignKey(x => x.CreatedByStudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
