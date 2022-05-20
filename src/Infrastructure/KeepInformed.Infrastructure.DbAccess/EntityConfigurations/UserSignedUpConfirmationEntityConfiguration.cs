using KeepInformed.Domain.Authorization.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeepInformed.Infrastructure.DbAccess.EntityConfigurations;

public class UserSignedUpConfirmationEntityConfiguration : BaseEntityConfiguration<UserSignedUpConfirmation>
{
    public void Configure(EntityTypeBuilder<UserSignedUpConfirmation> builder)
    {
        builder.HasIndex(x => new { x.UserId, x.IsActive })
            .IsUnique();
    }
}
