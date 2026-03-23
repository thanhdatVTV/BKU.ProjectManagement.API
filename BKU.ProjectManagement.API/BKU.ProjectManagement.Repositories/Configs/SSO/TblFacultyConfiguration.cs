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
    public class TblFacultyConfiguration : IEntityTypeConfiguration<TblFaculty>
    {
        public void Configure(EntityTypeBuilder<TblFaculty> builder)
        {
            builder.ToTable("TblFaculty", "SSO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FacultyName)
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

            builder.HasOne(x => x.Course)
                .WithMany(x => x.TblFaculties)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
