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
    public class ProjectTeamConfiguration : IEntityTypeConfiguration<ProjectTeam>
    {
        public void Configure(EntityTypeBuilder<ProjectTeam> builder)
        {
            builder.ToTable("ProjectTeam");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TeamCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.TeamName).HasMaxLength(255);
            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => x.TeamCode).IsUnique();

            builder.HasOne(x => x.ProjectPeriod)
                .WithMany(x => x.ProjectTeams)
                .HasForeignKey(x => x.ProjectPeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.LeaderStudent)
                .WithMany(x => x.LeadProjectTeams)
                .HasForeignKey(x => x.LeaderStudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AssignedLecturer)
                .WithMany(x => x.AssignedProjectTeams)
                .HasForeignKey(x => x.AssignedLecturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
