using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.BusinessLogic.Interfaces.Notification;
using DayPlanner.Backend.ApiModels;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationProvider _notificationProvider;
        private readonly INotificationService _notificationService;
        private readonly IUserContextService _userContextService;

        public NotificationController(
            INotificationProvider notificationProvider,
            INotificationService notificationService,
            IUserContextService userContextService)
        {
            _notificationProvider = notificationProvider;
            _notificationService = notificationService;
            _userContextService = userContextService;
        }

        [HttpGet(Name = nameof(GetUserNotifications))]
        public async Task<ActionResult<List<NotificationModel>>> GetUserNotifications()
        {
            var notifications = await _notificationProvider.GetUserNotifications();

            return Ok(notifications);
        }

        //[HttpPost(Name = nameof(CreateNotification))]
        //public async Task<ActionResult<NotificationModel>> CreateNotification(
        //    [FromBody] CreateNotificationModel notificationModel)
        //{
        //    var notificationId = await _notificationService.CreateNotification(notificationModel);
        //    var notification = await _notificationProvider.GetNotification(notificationId);

        //    return Ok(notification);
        //}



        [HttpDelete("{notificationId}", Name = nameof(DeleteNotification))]

        public async Task<ActionResult> DeleteNotification(
            [FromRoute] int notificationId)
        {
            await _notificationService.DeleteNotification(notificationId);
            return Ok();
        }

        [HttpDelete(Name = nameof(DeleteUserNotifications))]

        public async Task<ActionResult> DeleteUserNotifications()
        {
            await _notificationService.DeleteUserNotifications();
            return Ok();
        }
    }
}
