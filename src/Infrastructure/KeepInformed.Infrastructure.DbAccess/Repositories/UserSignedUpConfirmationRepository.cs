using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Application.Authorization.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Repositories;

public class UserSignedUpConfirmationRepository : BaseRepository<UserSignedUpConfirmation>, IUserSignedUpConfirmationRepository
{
    public UserSignedUpConfirmationRepository(KeepInformedContext context) : base(context)
    {
    }

    public async Task<UserSignedUpConfirmation?> GetActiveByUserId(Guid userId)
    {
        return await Entities.SingleOrDefaultAsync(x => x.UserId == userId && x.IsActive);
    }
}
