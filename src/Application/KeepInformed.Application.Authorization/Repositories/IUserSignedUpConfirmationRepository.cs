using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Domain.Authorization.Entities;

namespace KeepInformed.Application.Authorization.Repositories;

public interface IUserSignedUpConfirmationRepository : IBaseRepository<UserSignedUpConfirmation>
{
    Task<UserSignedUpConfirmation?> GetActiveByUserId(Guid userId);
}
