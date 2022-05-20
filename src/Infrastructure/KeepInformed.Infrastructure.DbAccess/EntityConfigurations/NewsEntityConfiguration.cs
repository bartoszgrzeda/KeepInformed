using KeepInformed.Domain.News.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations;

public class NewsEntityConfiguration : BaseEntityConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new { x.Source, x.CustomStringId });
    }
}
