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
    public class AppStudentConfiguration : IEntityTypeConfiguration<AppStudent>
    {
        public void Configure(EntityTypeBuilder<AppStudent> builder)
        {
            builder.ToTable("AppStudent");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StudentCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.FullName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Status).HasDefaultValue(1).IsRequired();
            builder.Property(x => x.LastSyncedAt).HasDefaultValueSql("GETDATE()").IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => x.SsoStudentId).IsUnique();
            builder.HasIndex(x => x.StudentCode).IsUnique();
            builder.HasIndex(x => x.AppUserId).IsUnique();

            builder.HasOne(x => x.AppUser)
                .WithOne(x => x.AppStudent)
                .HasForeignKey<AppStudent>(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Major)
                .WithMany(x => x.AppStudents)
                .HasForeignKey(x => x.MajorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ClassGroup)
                .WithMany(x => x.AppStudents)
                .HasForeignKey(x => x.ClassGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Faculty)
                .WithMany(x => x.AppStudents)
                .HasForeignKey(x => x.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Course)
                .WithMany(x => x.AppStudents)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
