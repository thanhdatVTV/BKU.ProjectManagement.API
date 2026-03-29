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
    public class RegistrationReviewHistoryConfiguration : IEntityTypeConfiguration<RegistrationReviewHistory>
    {
        public void Configure(EntityTypeBuilder<RegistrationReviewHistory> builder)
        {
            builder.ToTable("RegistrationReviewHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Action).IsRequired();
            builder.Property(x => x.ActionAt).HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(1000);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasOne(x => x.Registration)
                .WithMany(x => x.RegistrationReviewHistories)
                .HasForeignKey(x => x.RegistrationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ActionByUser)
                .WithMany(x => x.RegistrationReviewHistories)
                .HasForeignKey(x => x.ActionBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
