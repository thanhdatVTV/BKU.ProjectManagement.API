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
    public class ProjectPeriodConfiguration : IEntityTypeConfiguration<ProjectPeriod>
    {
        public void Configure(EntityTypeBuilder<ProjectPeriod> builder)
        {
            builder.ToTable("ProjectPeriod");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.AcademicYear).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Stage).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(1).IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasOne(x => x.Semester)
                .WithMany(x => x.ProjectPeriods)
                .HasForeignKey(x => x.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
