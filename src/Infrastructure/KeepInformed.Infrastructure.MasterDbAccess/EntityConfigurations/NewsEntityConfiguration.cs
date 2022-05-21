using KeepInformed.Domain.News.Entities;
using KeepInformed.Infrastructure.BaseDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations;

public class NewsEntityConfiguration : BaseEntityConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new { x.Source, x.CustomStringId });
    }
}
