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
    public class LecturerCapacityConfiguration : IEntityTypeConfiguration<LecturerCapacity>
    {
        public void Configure(EntityTypeBuilder<LecturerCapacity> builder)
        {
            builder.ToTable("LecturerCapacity");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MaxStudents).IsRequired();
            builder.Property(x => x.CurrentStudents).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.CurrentTeams).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.IsAvailable).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => new { x.ProjectPeriodId, x.LecturerId }).IsUnique();

            builder.HasOne(x => x.ProjectPeriod)
                .WithMany(x => x.LecturerCapacities)
                .HasForeignKey(x => x.ProjectPeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Lecturer)
                .WithMany(x => x.LecturerCapacities)
                .HasForeignKey(x => x.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Major)
                .WithMany(x => x.LecturerCapacities)
                .HasForeignKey(x => x.MajorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
