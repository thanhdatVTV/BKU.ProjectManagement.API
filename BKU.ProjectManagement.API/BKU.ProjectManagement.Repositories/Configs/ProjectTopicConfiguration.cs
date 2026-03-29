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
    public class ProjectTopicConfiguration : IEntityTypeConfiguration<ProjectTopic>
    {
        public void Configure(EntityTypeBuilder<ProjectTopic> builder)
        {
            builder.ToTable("ProjectTopic");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(500).IsRequired();
            builder.Property(x => x.TechnologyStack).HasMaxLength(1000);
            builder.Property(x => x.DomainType).HasMaxLength(255);
            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.RejectReason).HasMaxLength(1000);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasOne(x => x.ProjectTeam)
                .WithMany(x => x.ProjectTopics)
                .HasForeignKey(x => x.ProjectTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.ProjectTopics)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ApprovedByUser)
                .WithMany(x => x.ApprovedProjectTopics)
                .HasForeignKey(x => x.ApprovedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
