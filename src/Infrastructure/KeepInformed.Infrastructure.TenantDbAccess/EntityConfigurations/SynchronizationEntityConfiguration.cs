using KeepInformed.Domain.TenantNews.Entities;
using KeepInformed.Infrastructure.BaseDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.TenantDbAccess.EntityConfigurations;

public class SynchronizationEntityConfiguration : BaseEntityConfiguration<Synchronization>
{
    public void Configure(EntityTypeBuilder<Synchronization> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Date);
        builder.HasIndex(x => x.NewsSource);
    }
}
