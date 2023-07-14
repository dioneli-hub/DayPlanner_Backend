
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.Domain;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.Notification
{
    public interface INotificationService
    {
        Task<int> CreateNotification(CreateNotificationModel notificationModel);
        //Task<int> CreateNotification(CreateNotificationModel notificationModel, int userId);
        Task DeleteNotification(int notificationId);
        Task DeleteUserNotifications();
    }
}
