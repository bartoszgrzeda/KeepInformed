using KeepInformed.Domain.News.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations;

public class NewsEntityConfiguration : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.HasIndex(x => new { x.Source, x.CustomStringId });
    }
}
