using KeepInformed.Domain.MasterNews.Entities.Tvn;
using KeepInformed.Infrastructure.BaseDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations.Tvn;

public class TvnNewsEntityConfiguration : BaseEntityConfiguration<TvnNews>
{
    public void Configure(EntityTypeBuilder<TvnNews> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Guid)
            .IsUnique();
    }
}
