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
    public class StudentProjectRegistrationConfiguration : IEntityTypeConfiguration<StudentProjectRegistration>
    {
        public void Configure(EntityTypeBuilder<StudentProjectRegistration> builder)
        {
            builder.ToTable("StudentProjectRegistration");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.RejectReason).HasMaxLength(1000);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => new { x.ProjectPeriodId, x.StudentId }).IsUnique();

            builder.HasOne(x => x.ProjectPeriod)
                .WithMany(x => x.StudentProjectRegistrations)
                .HasForeignKey(x => x.ProjectPeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudentProjectRegistrations)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SelectedMajor)
                .WithMany(x => x.StudentProjectRegistrations)
                .HasForeignKey(x => x.SelectedMajorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ApprovedLecturer)
                .WithMany(x => x.ApprovedStudentProjectRegistrations)
                .HasForeignKey(x => x.ApprovedLecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReviewedByUser)
                .WithMany(x => x.ReviewedStudentProjectRegistrations)
                .HasForeignKey(x => x.ReviewedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
