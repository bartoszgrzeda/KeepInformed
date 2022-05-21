using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Application.Authorization.Repositories;
using Microsoft.EntityFrameworkCore;
using KeepInformed.Infrastructure.BaseDbAccess.Repositories;

namespace KeepInformed.Infrastructure.MasterDbAccess.Repositories;

public class UserRepository : BaseMasterRepository<User>, IUserRepository
{
    public UserRepository(MasterKeepInformedDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await Entities.SingleOrDefaultAsync(x => x.Email == email);
    }
}
