using AutoMapper;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class NotificationProvider : INotificationProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        
        public NotificationProvider(
            DataContext context,
            IMapper mapper,
            IUserContextService userContextService) 
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<NotificationModel> GetNotification(int notificationId)
        {
            var notification = await _context.Notifications
                .Include(x => x.User)
                .Where(b => b.Id == notificationId)
                .FirstOrDefaultAsync();

            var notificationModel = _mapper.Map<NotificationModel>(notification);

            return notificationModel;
        }

        public async Task<List<NotificationModel>> GetUserNotifications()
        {
            var currentUserId =  _userContextService.GetCurrentUserId();
            var notifications = await _context.Notifications
                .Include(x => x.User)
                .Where(b => b.UserId == currentUserId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var notificationModels = _mapper.Map<List<NotificationModel>>(notifications);

            return notificationModels;
        }
    }
}
