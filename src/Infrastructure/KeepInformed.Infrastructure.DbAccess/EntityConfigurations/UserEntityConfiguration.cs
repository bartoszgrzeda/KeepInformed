using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Domain.News.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
