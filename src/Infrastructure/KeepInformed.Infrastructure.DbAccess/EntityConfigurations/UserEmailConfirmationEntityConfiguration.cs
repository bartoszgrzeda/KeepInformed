using KeepInformed.Domain.Authorization.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations;

public class UserEmailConfirmationEntityConfiguration : BaseEntityConfiguration<UserEmailConfirmation>
{
    public void Configure(EntityTypeBuilder<UserEmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.HasIndex(x => new { x.UserId, x.IsActive })
            .IsUnique();
    }
}
