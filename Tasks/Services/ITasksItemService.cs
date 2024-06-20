using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Models;

namespace Tasks.Services
{
    public interface ITasksItemService
    {
        Task<TasksModel[]> GetIncompleteItemsAsync();

        Task<bool> AddItemsAsync(TasksModel tasksModel);

        Task<bool> MarkDoneAsync(Guid id);
        
    }
}
