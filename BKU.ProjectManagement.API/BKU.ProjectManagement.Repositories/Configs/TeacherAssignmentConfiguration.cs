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
    public class TeacherAssignmentConfiguration : IEntityTypeConfiguration<TeacherAssignment>
    {
        public void Configure(EntityTypeBuilder<TeacherAssignment> builder)
        {
            builder.ToTable("TeacherAssignment");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AssignedAt).HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(1).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasOne(x => x.ProjectPeriod)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.ProjectPeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProjectTeam)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.ProjectTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedByUser)
                .WithMany(x => x.TeacherAssignments)
                .HasForeignKey(x => x.AssignedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
