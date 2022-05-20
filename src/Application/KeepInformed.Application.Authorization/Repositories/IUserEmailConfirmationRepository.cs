using KeepInformed.Common.Domain.Repositories;
using KeepInformed.Domain.Authorization.Entities;

namespace KeepInformed.Application.Authorization.Repositories;

public interface IUserEmailConfirmationRepository : IBaseRepository<UserEmailConfirmation>
{
    Task<UserEmailConfirmation?> GetActiveByUserId(Guid userId);
    Task<UserEmailConfirmation?> GetActiveByIdAndUserId(Guid id, Guid userId);
}
