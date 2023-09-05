
using AutoMapper;
using DayPlanner.Backend.ApiModels.TaskItem;
using DayPlanner.Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayPlanner.Backend.BusinessLogic.Interfaces.Recurrence
{
    public interface IRecurrenceProvider
    {
       
        Task<List<TaskItemModel>> GetChildTasks(int parentTaskId);
    }
}
