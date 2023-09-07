using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUserContextService _userContextService;
        private readonly DataContext _context;
        public NotificationService(
            IUserContextService userContextService,
            DataContext context
            )
        {
            _userContextService = userContextService;
            _context = context;

        }
        public async Task<int> CreateNotification(CreateNotificationModel notificationModel)
        {
            var user = await _context.Users.FindAsync(notificationModel.UserId);


            var notification = new Notification
            {
                Text = notificationModel.Text,
                UserId = notificationModel.UserId,
                User = user,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification.Id;
        }


        public async Task DeleteNotification(int notificationId)
        {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var notification = await _context.Notifications
                    .FirstOrDefaultAsync(x => x.Id == notificationId);

                if (notification == null)
                {
                    throw new ApplicationException("Notification not found.");
                }

                if (notification.UserId != currentUserId)
                {
                    throw new ApplicationException("Access denied.");
                }


                _context.Notifications.Remove(notification);

                await _context.SaveChangesAsync();
            } catch
            {
                throw new ApplicationException("Some error has occured while deleting the notification...");
            }
            
        }

        public async Task DeleteUserNotifications() {
            try
            {
                var currentUserId = _userContextService.GetCurrentUserId();
                var notifications = await _context.Notifications
                    .Where(x => x.UserId == currentUserId)
                    .ToListAsync();

                if (notifications == null)
                {
                    throw new ApplicationException("Not found.");
                }

                _context.Notifications.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new ApplicationException("Some error has occured while deleting user's notifications...");
            }
           
        }
    }
}
