using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces.Notification;
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
            var currentUserId = _userContextService.GetCurrentUserId();
            var currentUser = await _context.Users.FindAsync(currentUserId);


            var notification = new Notification
            {
                Text = notificationModel.Text,
                UserId = currentUserId,
                User = currentUser,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            return notification.Id;
        }

        public async Task DeleteNotification(int notificationId)
        {
            var currentUserId = _userContextService.GetCurrentUserId();
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(x => x.Id == notificationId);

            if (notification == null)
            {
                throw new ApplicationException("Board not found.");
            }

            if (notification.UserId != currentUserId)
            {
                throw new ApplicationException("Access denied.");
            }

            
            _context.Notifications.Remove(notification);

            await _context.SaveChangesAsync();
        }
    }
}
