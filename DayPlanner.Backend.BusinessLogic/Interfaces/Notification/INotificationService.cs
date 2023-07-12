
using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.Notification
{
    public interface INotificationService
    {
        Task<int> CreateNotification(CreateNotificationModel notificationModel);
        Task DeleteNotification(int notificationId);
        Task DeleteUserNotifications();
    }
}
