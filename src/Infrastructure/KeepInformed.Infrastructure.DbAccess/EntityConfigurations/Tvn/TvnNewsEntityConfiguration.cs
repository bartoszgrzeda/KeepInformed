using KeepInformed.Domain.Tvn.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations.Tvn;

public class TvnNewsEntityConfiguration : IEntityTypeConfiguration<TvnNews>
{
    public void Configure(EntityTypeBuilder<TvnNews> builder)
    {
        builder.Property(x => x.TvnGuid)
            .IsRequired();

        builder.HasIndex(x => x.TvnGuid)
            .IsUnique();
    }
}
