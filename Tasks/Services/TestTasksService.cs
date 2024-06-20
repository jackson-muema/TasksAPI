/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Models;

namespace Tasks.Services
{
    public class TestTasksService : ITasksItemService
    {

       public Task<TasksModel[]> GetIncompleteItemsAsync()
        {
            var item1 = new TasksModel
            {
                Title = "Learn ASP.NET core",
                DueAt = DateTimeOffset.Now.AddDays(1),
            };

            var item2 = new TasksModel
            {
                Title = "Build awesome apps",
                DueAt = DateTimeOffset.Now.AddDays(2),
            };
            return Task.FromResult(new[] { item1, item2 });

        }

    }
}
*/