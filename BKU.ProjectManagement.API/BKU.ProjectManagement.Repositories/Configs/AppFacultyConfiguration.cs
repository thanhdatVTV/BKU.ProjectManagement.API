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
    public class AppFacultyConfiguration : IEntityTypeConfiguration<AppFaculty>
    {
        public void Configure(EntityTypeBuilder<AppFaculty> builder)
        {
            builder.ToTable("AppFaculty");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FacultyName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LastSyncedAt).HasDefaultValueSql("GETDATE()").IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => x.SsoFacultyId).IsUnique();

            builder.HasOne(x => x.Course)
                .WithMany(x => x.AppFaculties)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
