using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tasks.Data;
using Tasks.Models;
using Tasks.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Tasks.Services
{
    public class TasksItemService : ITasksItemService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;
        public TasksItemService(ApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;

        }
        public async Task<List<TasksDTO>> GetIncompleteItemsAsync()
        {
              var items = await _context.Items
                .Where(x => x.IsDone == false)
                .ToListAsync();
            return _mapper.Map<List<TasksDTO>>(items);
        }

        public async Task<bool> AddItemsAsync(TasksDTO tasksDTO)
        {
            var tasksModel = _mapper.Map<TasksModel>(tasksDTO);

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
