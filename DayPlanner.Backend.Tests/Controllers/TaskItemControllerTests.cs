

using DayPlanner.Backend.ApiModels.Recurrence;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces.Recurrence;
using DayPlanner.Backend.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace DayPlanner.Backend.Tests.Controllers
{
    public class TaskItemControllerTests
    {
        private readonly ITaskItemProvider _taskItemProvider;
        private readonly ITaskItemService _taskItemService;
        private readonly IRecurrenceProvider _recurrenceProvider;
        private readonly IRecurrenceService _recurrenceService;

        public TaskItemControllerTests()
        {
            _taskItemProvider = A.Fake<ITaskItemProvider>();
            _taskItemService = A.Fake<ITaskItemService>();
            _recurrenceProvider = A.Fake<IRecurrenceProvider>();
            _recurrenceService = A.Fake<IRecurrenceService>();
            
        }

        [Fact]
        public async void TaskItemController_GetBoardTasks_ReturnOkAndTaskItemModelList()
        {
            //Arrange
            int boardId = 1;
            bool ifMyTasks = true;
            A.CallTo(() => _taskItemProvider.GetBoardTasks(boardId, ifMyTasks)).Returns(new List<TaskItemModel>());
            var controller = new TaskItemController( _taskItemProvider, _taskItemService,  _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.GetBoardTasks(boardId, ifMyTasks);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<TaskItemModel>>));
        }

        [Fact]
        public async void TaskItemController_GetChildTasks_ReturnOkAndTaskItemModelList()
        {
            //Arrange
            int parentTaskId = 1;
            A.CallTo(() => _recurrenceProvider.GetChildTasks(parentTaskId)).Returns(new List<TaskItemModel>());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.GetChildTasks(parentTaskId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<TaskItemModel>>));
        }


        [Fact]
        public async void TaskItemController_UpdateTask_ReturnOkAndTaskItemModel()
        {
            //Arrange
            int taskId = 1;
            int updatedTaskId = 1;
            EditTaskItemModel editedTaskModel = A.Fake<EditTaskItemModel>();
            A.CallTo(() => _taskItemService.UpdateTask(taskId, editedTaskModel)).Returns(new int());
            A.CallTo(() => _taskItemProvider.GetTask(updatedTaskId)).Returns(new TaskItemModel());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.UpdateTask(taskId, editedTaskModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<TaskItemModel>));
        }

        [Fact]
        public async void TaskItemController_UpdateTaskPerformer_ReturnOkAndTaskItemModel()
        {
            //Arrange
            int taskId = 1;
            int newPerformerId = 2;
            A.CallTo(() => _taskItemService.UpdateTaskPerformer(taskId, newPerformerId)).Returns(Task.CompletedTask);
            A.CallTo(() => _taskItemProvider.GetTask(taskId)).Returns(new TaskItemModel());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.UpdateTaskPerformer(taskId, newPerformerId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<TaskItemModel>));
        }


        [Fact]
        public async void TaskItemController_UpdateTaskOverdue_ReturnOkAndTaskItemModel()
        {
            //Arrange
            int taskId = 1;
            A.CallTo(() => _taskItemService.UpdateTaskOverdue(taskId)).Returns(Task.CompletedTask);
            A.CallTo(() => _taskItemProvider.GetTask(taskId)).Returns(new TaskItemModel());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.UpdateTaskOverdue(taskId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<TaskItemModel>));
        }


        [Fact]
        public async void TaskItemController_DeleteTask_ReturnOk()
        {
            //Arrange
            int taskId = 1;
            A.CallTo(() => _taskItemService.DeleteTask(taskId)).Returns(Task.CompletedTask);
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.DeleteTask(taskId);
            var okResult = result as OkResult;
            //Assert
            okResult.Should().NotBeNull();
        }


        [Fact]
        public async void TaskItemController_GetUserBoardsTasks_ReturnOkAndTaskItemModelList()
        {
            //Arrange
            int userId = 1;
            A.CallTo(() => _taskItemProvider.GetUserBoardsTasks(userId)).Returns(new List<TaskItemModel>());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.GetUserBoardsTasks(userId);
            
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<TaskItemModel>>));
        }


        [Fact]
        public async void TaskItemController_CompleteTask_ReturnOk()
        {
            //Arrange
            int taskId = 1;
            A.CallTo(() => _taskItemService.CompleteTask(taskId)).Returns(Task.CompletedTask);
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.CompleteTask(taskId);
            var okResult = result as OkObjectResult;
            //Assert
            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be("Task completed.");
        }

        [Fact]
        public async void TaskItemController_MarkTaskAsToDo_ReturnOk()
        {
            //Arrange
            int taskId = 1;
            A.CallTo(() => _taskItemService.MarkTaskAsToDo(taskId)).Returns(Task.CompletedTask);
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.MarkTaskAsToDo(taskId);
            var okResult = result as OkObjectResult;
            //Assert
            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be("Task marked as ToDo.");
        }

        [Fact]
        public async void TaskItemController_UpdateChangeRecurredChildren_ReturnOkAndBool()
        {
            //Arrange
            int taskId = 1;
            A.CallTo(() => _taskItemService.UpdateChangeRecurredChildren(taskId)).Returns(new bool());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.UpdateChangeRecurredChildren(taskId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<bool>));
        }


        [Fact]
        public async void TaskItemController_AddRecurrence_ReturnOkAndTaskItemModelList()
        {
            //Arrange
            int patternId = 1;
            RecurringPatternModel patternModel = A.Fake<RecurringPatternModel>();
            A.CallTo(() => _recurrenceService.AddRecurrence(patternModel)).Returns(new int());
            A.CallTo(() => _recurrenceService.GenerateChildTasks(patternId)).Returns(new List<TaskItemModel>());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.AddRecurrence(patternModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<TaskItemModel>>));
        }


        [Fact]
        public async void TaskItemController_GetBoardTasksGroupedByPerformer_ReturnOkAndTaskGroupList()
        {
            //Arrange
            int boardId = 1;
            RecurringPatternModel patternModel = A.Fake<RecurringPatternModel>();
            A.CallTo(() => _taskItemProvider.GetBoardTasksGroupedByPerformer(boardId)).Returns(new List<TaskGroup<UserModel>>());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.GetBoardTasksGroupedByPerformer(boardId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<TaskGroup<UserModel>>>));
        }


        [Fact]
        public async void TaskItemController_GetBoardTasksGroupedByCompleted_ReturnOkAndTaskGroupList()
        {
            //Arrange
            int boardId = 1;
            bool ifMyTasks = false;
            A.CallTo(() => _taskItemProvider.GetBoardTasksGroupedByCompleted(boardId, ifMyTasks)).Returns(new List<TaskGroup<bool>>());
            var controller = new TaskItemController(_taskItemProvider, _taskItemService, _recurrenceService, _recurrenceProvider);

            //Act
            var result = await controller.GetBoardTasksGroupedByCompleted(boardId, ifMyTasks);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(ActionResult<List<TaskGroup<bool>>>));
        }
    }
}
