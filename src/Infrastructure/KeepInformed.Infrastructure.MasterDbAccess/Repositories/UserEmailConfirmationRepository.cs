using KeepInformed.Domain.Authorization.Entities;
using Microsoft.EntityFrameworkCore;
using KeepInformed.Infrastructure.BaseDbAccess.Repositories;
using KeepInformed.Application.Authorization.Repositories;

namespace KeepInformed.Infrastructure.MasterDbAccess.Repositories;

public class UserEmailConfirmationRepository : BaseMasterRepository<UserEmailConfirmation>, IUserEmailConfirmationRepository
{
    public UserEmailConfirmationRepository(MasterKeepInformedDbContext context) : base(context)
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
