using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Infrastructure.BaseDbAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.MasterDbAccess.EntityConfigurations;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
