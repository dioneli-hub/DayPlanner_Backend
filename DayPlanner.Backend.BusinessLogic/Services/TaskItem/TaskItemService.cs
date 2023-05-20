using DayPlanner.Backend.BusinessLogic.Interfaces;
using DayPlanner.Backend.BusinessLogic.Interfaces.Context;
using DayPlanner.Backend.DataAccess;

namespace DayPlanner.Backend.BusinessLogic.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public TaskItemService(DataContext context,
            IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }


    }
}
