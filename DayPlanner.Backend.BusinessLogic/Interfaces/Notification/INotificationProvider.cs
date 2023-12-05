
using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface INotificationProvider
    {
        Task<List<NotificationModel>> GetUserNotifications();
        Task<NotificationModel> GetNotification(int notificationId);
    }
}