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
    public class TblStudentConfiguration : IEntityTypeConfiguration<TblStudent>
    {
        public void Configure(EntityTypeBuilder<TblStudent> builder)
        {
            builder.ToTable("TblStudent", "SSO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StudentId)
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

            builder.HasIndex(x => x.StudentId)
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.TblStudents)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Major)
                .WithMany(x => x.TblStudents)
                .HasForeignKey(x => x.MajorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ClassGroup)
                .WithMany(x => x.TblStudents)
                .HasForeignKey(x => x.ClassGroupId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
