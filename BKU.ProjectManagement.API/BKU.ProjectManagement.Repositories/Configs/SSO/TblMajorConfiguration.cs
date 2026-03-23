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
    public class TblMajorConfiguration : IEntityTypeConfiguration<TblMajor>
    {
        public void Configure(EntityTypeBuilder<TblMajor> builder)
        {
            builder.ToTable("TblMajor", "SSO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.MajorName)
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

            builder.HasOne(x => x.Faculty)
                .WithMany(x => x.TblMajors)
                .HasForeignKey(x => x.FacultyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
