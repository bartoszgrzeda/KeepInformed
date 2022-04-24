using KeepInformed.Domain.Authorization.Entities;
using KeepInformed.Application.Authorization.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KeepInformed.Infrastructure.DbAccess.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(KeepInformedContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await Entities.SingleOrDefaultAsync(x => x.Email == email);
    }
}
