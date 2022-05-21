using KeepInformed.Application.Authorization.Repositories;
using KeepInformed.Domain.Authorization.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Mocks;

public class UserRepositoryMock : BaseRespositoryMock<User>, IUserRepository
{
    public async Task<User?> GetByEmail(string email)
    {
        return _storage.SingleOrDefault(x => x.Email == email);
    }
}
