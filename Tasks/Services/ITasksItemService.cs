using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Controllers;
using Tasks.Models;

namespace Tasks.Services
{
    public interface ITasksItemService
    {

        Task<List<TasksDTO>> GetIncompleteItemsAsync(IdentityUser user);
        Task<bool> AddItemsAsync(TasksDTO tasksDTO, IdentityUser user);

        Task<bool> MarkDoneAsync(Guid id, IdentityUser user);
        
    }
}
