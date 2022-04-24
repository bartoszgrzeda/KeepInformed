using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Domain.Authorization.Entities;

namespace KeepInformed.Application.Authorization.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<User?> GetByEmail(string email);
}
