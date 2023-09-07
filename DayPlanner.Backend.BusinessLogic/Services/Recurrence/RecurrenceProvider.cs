using AutoMapper;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.BusinessLogic.Interfaces.Recurrence;
using DayPlanner.Backend.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DayPlanner.Backend.BusinessLogic.Services.Recurrence
{
    public class RecurrenceProvider : IRecurrenceProvider
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RecurrenceProvider(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TaskItemModel>> GetChildTasks(int parentTaskId)
        {
            var childTasks = await _context.TaskItems
                                         .Where(x => x.ParentTaskId == parentTaskId)
                                         .Include(t => t.Performer)
                                         .Include(t => t.Creator)
                                         .Include(t => t.Board)
                                         .ToListAsync();

            var childTaskModels = _mapper.Map<List<TaskItemModel>>(childTasks);

            return childTaskModels;
        }
    }
}
