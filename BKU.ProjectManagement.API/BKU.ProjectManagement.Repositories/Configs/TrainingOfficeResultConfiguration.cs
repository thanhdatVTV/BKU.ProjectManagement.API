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
    public class TrainingOfficeResultConfiguration : IEntityTypeConfiguration<TrainingOfficeResult>
    {
        public void Configure(EntityTypeBuilder<TrainingOfficeResult> builder)
        {
            builder.ToTable("TrainingOfficeResult");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FinalScore).HasColumnType("decimal(5,2)");
            builder.Property(x => x.ResultStatus).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.EvaluationNote).HasMaxLength(2000);
            builder.Property(x => x.ReceiveStatus).HasDefaultValue(0).IsRequired();
            builder.Property(x => x.ExternalRefNo).HasMaxLength(100);

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);

            builder.HasIndex(x => x.ProjectTeamId).IsUnique();

            builder.HasOne(x => x.ProjectPeriod)
                .WithMany(x => x.TrainingOfficeResults)
                .HasForeignKey(x => x.ProjectPeriodId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProjectTeam)
                .WithMany(x => x.TrainingOfficeResults)
                .HasForeignKey(x => x.ProjectTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Teacher)
                .WithMany(x => x.TrainingOfficeResults)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SentByUser)
                .WithMany(x => x.SentTrainingOfficeResults)
                .HasForeignKey(x => x.SentBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
