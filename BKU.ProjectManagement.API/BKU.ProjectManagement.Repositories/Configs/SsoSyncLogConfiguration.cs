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
    public class SsoSyncLogConfiguration : IEntityTypeConfiguration<SsoSyncLog>
    {
        public void Configure(EntityTypeBuilder<SsoSyncLog> builder)
        {
            builder.ToTable("SsoSyncLog");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SyncType).HasMaxLength(50).IsRequired();
            builder.Property(x => x.SsoEntityId).HasMaxLength(100);
            builder.Property(x => x.Message).HasMaxLength(2000);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.SyncedAt).HasDefaultValueSql("GETDATE()").IsRequired();

            builder.Property(x => x.CreatedBy).HasMaxLength(100).IsRequired().HasDefaultValue("system");
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedBy).HasMaxLength(100);
        }
    }
}
