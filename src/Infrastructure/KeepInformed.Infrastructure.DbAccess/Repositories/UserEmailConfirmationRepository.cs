using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Application.Authorization.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Repositories;

public class UserEmailConfirmationRepository : BaseRepository<UserEmailConfirmation>, IUserEmailConfirmationRepository
{
    public UserEmailConfirmationRepository(KeepInformedContext context) : base(context)
    {
    }

    public async Task<UserEmailConfirmation?> GetActiveByIdAndUserId(Guid id, Guid userId)
    {
        return await Entities.SingleOrDefaultAsync(x => x.Id == id && x.UserId == userId && x.IsActive);
    }

    public async Task<UserEmailConfirmation?> GetActiveByUserId(Guid userId)
    {
        return await Entities.SingleOrDefaultAsync(x => x.UserId == userId && x.IsActive);
    }
}
