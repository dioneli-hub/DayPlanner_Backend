﻿using DayPlanner.Backend.Api.Interfaces;
using DayPlanner.Backend.DataAccess;
using DayPlanner.Backend.DataAccess.Entities;

namespace DayPlanner.Backend.Api.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly DataContext _context;

        public TaskItemRepository(DataContext context)
        {
            _context = context;
        }

        ICollection<TaskItem> ITaskItemRepository.GetTasks()
        {
            return _context.TaskItems
                .OrderBy(t => t.Id).ToList();
        }

        public ICollection<TaskItem> GetTodaysTasks()
        {
            // to-do: create a separate method for checking if one particular task is today's
            return _context.TaskItems
                //.Where(t => IsTaskForToday(t.DueDate))
                .Where (item => item.DueDate >= DateTime.Now.Date &&
                         item.DueDate <= DateTime.Now.AddDays(1))
                .OrderBy(t => t.Id).ToList();
        }

        public TaskItem GetTaskItem(int taskItemId)
        {
            return _context.TaskItems
                .Where(t => t.Id == taskItemId)
                .FirstOrDefault();
        }

        public bool TaskItemExists(int taskItemId)
        {
            return _context.TaskItems.Any(t => t.Id == taskItemId);
        }

        public bool UpdateTask(TaskItem task)
        {
            _context.Update(task);
            return Save();
        }
        public bool DeleteTaskItem(TaskItem task)
        {
            _context.Remove(task);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


        
    }
}
