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
    public class ProgressReportAttachmentConfiguration : IEntityTypeConfiguration<ProgressReportAttachment>
    {
        public void Configure(EntityTypeBuilder<ProgressReportAttachment> builder)
        {
            builder.ToTable("ProgressReportAttachment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.FileUrl).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.FileType).HasMaxLength(50);
            builder.Property(x => x.UploadedAt).HasDefaultValueSql("GETDATE()").IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasOne(x => x.ProgressReport)
                .WithMany(x => x.ProgressReportAttachments)
                .HasForeignKey(x => x.ProgressReportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UploadedByUser)
                .WithMany(x => x.ProgressReportAttachments)
                .HasForeignKey(x => x.UploadedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
