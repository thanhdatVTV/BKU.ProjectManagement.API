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
    public class ProjectTeamMemberConfiguration : IEntityTypeConfiguration<ProjectTeamMember>
    {
        public void Configure(EntityTypeBuilder<ProjectTeamMember> builder)
        {
            builder.ToTable("ProjectTeamMember");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.JoinedAt).HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(x => x.IsActiveMember).HasDefaultValue(true).IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => new { x.ProjectTeamId, x.StudentId }).IsUnique();

            builder.HasOne(x => x.ProjectTeam)
                .WithMany(x => x.ProjectTeamMembers)
                .HasForeignKey(x => x.ProjectTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Student)
                .WithMany(x => x.ProjectTeamMembers)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
