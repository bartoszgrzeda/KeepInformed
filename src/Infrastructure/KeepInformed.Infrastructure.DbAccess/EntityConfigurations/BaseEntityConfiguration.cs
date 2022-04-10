using KeepInformed.Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired();

        builder.HasKey(x => x.Id);
    }
}
