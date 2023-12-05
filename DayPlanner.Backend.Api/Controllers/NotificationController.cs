using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DayPlanner.Backend.ApiModels;
using DayPlanner.Backend.BusinessLogic.Interfaces;

namespace DayPlanner.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly INotificationProvider _notificationProvider;
        private readonly INotificationService _notificationService;

        public NotificationController(
            INotificationProvider notificationProvider,
            INotificationService notificationService
            )
        {
            _notificationProvider = notificationProvider;
            _notificationService = notificationService;
        }

        [HttpGet(Name = nameof(GetUserNotifications))]
        public async Task<ActionResult<List<NotificationModel>>> GetUserNotifications()
        {
            var notifications = await _notificationProvider.GetUserNotifications();

            return Ok(notifications);
        }


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
