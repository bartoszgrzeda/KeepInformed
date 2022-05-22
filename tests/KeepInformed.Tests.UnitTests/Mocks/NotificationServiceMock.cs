using KeepInformed.Common.Notifications;
using System.Threading.Tasks;

namespace KeepInformed.Tests.UnitTests.Mocks
{
    public class NotificationServiceMock : INotificationService
    {
        public async Task NotifyAllUsers(string method, object? data = null)
        {
        }

        public async Task NotifyCurentUser(string method, object? data = null)
        {
        }
    }
}
