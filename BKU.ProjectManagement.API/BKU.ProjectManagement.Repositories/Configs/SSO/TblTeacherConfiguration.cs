using BKU.ProjectManagement.Repositories.Entities.SSO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKU.ProjectManagement.Repositories.Configs.SSO
{
    public class TblTeacherConfiguration : IEntityTypeConfiguration<TblTeacher>
    {
        public void Configure(EntityTypeBuilder<TblTeacher> builder)
        {
            builder.ToTable("TblTeacher", "SSO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TeacherId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(x => x.CreatedBy)
                .HasMaxLength(100)
                .HasDefaultValue("system")
                .IsRequired();

            builder.Property(x => x.UpdatedBy)
                .HasMaxLength(100);

            builder.Property(x => x.IsDelete)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasIndex(x => x.TeacherId)
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.TblTeachers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Course)
                .WithMany(x => x.TblTeachers)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
