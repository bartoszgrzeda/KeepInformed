using KeepInformed.Application.MasterNews.Repositories.Tvn;
using KeepInformed.Domain.MasterNews.Entities.Tvn;
using System.Linq;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Mocks;

public class TvnNewsRepositoryMock : BaseRespositoryMock<TvnNews>, ITvnNewsRepository
{
    public async Task<TvnNews?> GetByGuid(string guid)
    {
        return _storage.SingleOrDefault(x => x.Guid == guid);
    }
}
