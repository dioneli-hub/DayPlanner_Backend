
using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task<int> CreateNotification(CreateNotificationModel notificationModel);
        //Task<int> CreateNotification(CreateNotificationModel notificationModel, int userId);
        Task DeleteNotification(int notificationId);
        Task DeleteUserNotifications();
    }
}
