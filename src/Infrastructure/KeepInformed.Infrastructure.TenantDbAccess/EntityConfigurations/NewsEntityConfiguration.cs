using KeepInformed.Domain.TenantNews.Entities;
using KeepInformed.Infrastructure.BaseDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.TenantDbAccess.EntityConfigurations;

public class NewsEntityConfiguration : BaseEntityConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.PublicationDate);
        builder.HasIndex(x => x.Source);
    }
}
