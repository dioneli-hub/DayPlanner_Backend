using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Interfaces
{
    public interface ITaskItemService
    {
        Task DeleteTask(int taskId);
    }
}
