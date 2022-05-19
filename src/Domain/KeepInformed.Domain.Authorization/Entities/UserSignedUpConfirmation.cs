using KeepInformed.Common.Domain.Entities;

namespace KeepInformed.Domain.Authorization.Entities;

public class UserSignedUpConfirmation : BaseEntity
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public bool IsActive { get; private set; }

    public UserSignedUpConfirmation(Guid confirmationId, Guid userId) : base(confirmationId)
    {
        UserId = userId;
        IsActive = true;
    }

    public void SetIsActive(bool isActive)
    {
        if (IsActive == isActive)
        {
            return;
        }

        IsActive = isActive;
    }
}
