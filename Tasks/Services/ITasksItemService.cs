using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Models;

namespace Tasks.Services
{
    public interface ITasksItemService
    {
        //Task<TasksModel[]> GetIncompleteItemsAsync();
        Task<List<TasksDTO>> GetIncompleteItemsAsync();
        Task<bool> AddItemsAsync(TasksDTO tasksDTO);

        Task<bool> MarkDoneAsync(Guid id);
        
    }
}
