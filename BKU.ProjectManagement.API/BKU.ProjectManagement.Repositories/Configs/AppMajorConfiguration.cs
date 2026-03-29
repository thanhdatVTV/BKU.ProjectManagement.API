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
    public class AppMajorConfiguration : IEntityTypeConfiguration<AppMajor>
    {
        public void Configure(EntityTypeBuilder<AppMajor> builder)
        {
            builder.ToTable("AppMajor");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MajorName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.LastSyncedAt).HasDefaultValueSql("GETDATE()").IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => x.SsoMajorId).IsUnique();

            builder.HasOne(x => x.Faculty)
                .WithMany(x => x.AppMajors)
                .HasForeignKey(x => x.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
