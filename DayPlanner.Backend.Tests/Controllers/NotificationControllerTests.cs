
using Microsoft.AspNetCore.Mvc;



namespace DayPlanner.Backend.Tests.Controllers
{
    public class NotificationControllerTests
    {
        private readonly INotificationProvider _notificationProvider;
        private readonly INotificationService _notificationService;


        public NotificationControllerTests()
        {
            _notificationProvider = A.Fake<INotificationProvider>(); 
            _notificationService = A.Fake<INotificationService>();
        }

        [Fact]
        public async void NotificationController_GetUserNotifications_ReturnOkAndNotificationModelList()
        {
            //Arrange
            A.CallTo(() => _notificationProvider.GetUserNotifications()).Returns(new List<NotificationModel>());
            var controller = new NotificationController(_notificationProvider, _notificationService);

            //Act
            var result = await controller.GetUserNotifications();


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<NotificationModel>>));
        }


        [Fact]
        public async void NotificationController_DeleteNotification_ReturnOk()
        {
            int notificationId = 1;

            //Arrange
            A.CallTo(() => _notificationService.DeleteNotification(notificationId)).GetType().Should().NotBeNull(); ;
            var controller = new NotificationController(_notificationProvider, _notificationService);

            //Act
            var result = await controller.DeleteNotification(notificationId);


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async void NotificationController_DeleteUserNotifications_ReturnOk()
        {

            //Arrange
            A.CallTo(() => _notificationService.DeleteUserNotifications()).GetType().Should().NotBeNull(); ;
            var controller = new NotificationController(_notificationProvider, _notificationService);

            //Act
            var result = await controller.DeleteUserNotifications();


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkResult));
        }
    }
}
