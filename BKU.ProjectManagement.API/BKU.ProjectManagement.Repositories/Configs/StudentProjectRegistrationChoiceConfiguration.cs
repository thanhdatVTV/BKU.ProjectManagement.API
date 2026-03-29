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
    public class StudentProjectRegistrationChoiceConfiguration : IEntityTypeConfiguration<StudentProjectRegistrationChoice>
    {
        public void Configure(EntityTypeBuilder<StudentProjectRegistrationChoice> builder)
        {
            builder.ToTable("StudentProjectRegistrationChoice");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => new { x.RegistrationId, x.PriorityOrder }).IsUnique();
            builder.HasIndex(x => new { x.RegistrationId, x.LecturerId }).IsUnique();

            builder.HasOne(x => x.Registration)
                .WithMany(x => x.StudentProjectRegistrationChoices)
                .HasForeignKey(x => x.RegistrationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Lecturer)
                .WithMany(x => x.StudentProjectRegistrationChoices)
                .HasForeignKey(x => x.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
