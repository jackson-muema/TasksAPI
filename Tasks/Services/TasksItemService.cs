using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tasks.Data;
using Tasks.Models;
using Tasks.Services;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Services
{
    public class TasksItemService : ITasksItemService
    {
        private readonly ApplicationDbContext _context;
        public TasksItemService(ApplicationDbContext context)
        {

            _context = context;

        }
        public async Task<TasksModel[]> GetIncompleteItemsAsync()
        {
            return await _context.Items
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
        }

        public async Task<bool> AddItemsAsync(TasksModel tasksModel)
        {
            tasksModel.Id = Guid.NewGuid();

            tasksModel.IsDone = false;

            tasksModel.DueAt = DateTime.Now.AddDays(3);

            _context.Items.Add(tasksModel);

            var saveResult = await _context.SaveChangesAsync();

            return (saveResult) == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.Items
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var SaveResult = await _context.SaveChangesAsync();

            return SaveResult == 1;

        }
    }
}
