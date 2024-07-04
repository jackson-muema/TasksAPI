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
using Tasks.Controllers;
using Microsoft.AspNetCore.Identity;

namespace Tasks.Services
{
    public class TasksItemService : ITasksItemService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly UserManager<IdentityUser> _userManager;

        public TasksItemService(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {

            _context = context;
            _mapper = mapper;
            _userManager = userManager;


        }
        public async Task<List<TasksDTO>> GetIncompleteItemsAsync(IdentityUser user)
        {
              var items = await _context.Items
                .Where(x => x.IsDone == false && x.UserId == user.Id)
                .ToArrayAsync();
            return _mapper.Map<List<TasksDTO>>(items);
        }

        public async Task<bool> AddItemsAsync(TasksDTO tasksDTO, IdentityUser user)
        {
            var tasksModel = _mapper.Map<TasksModel>(tasksDTO);

            /*tasksModel.Id = Guid.NewGuid();

            tasksModel.IsDone = false;*/

            tasksModel.UserId = user.Id;

            tasksModel.DueAt = DateTime.Now.AddDays(3);

            _context.Items.Add(tasksModel);

            var saveResult = await _context.SaveChangesAsync();

            return (saveResult) == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
                .Where(x => x.Id == id && x.UserId == user.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var SaveResult = await _context.SaveChangesAsync();

            return SaveResult == 1;

        }
    }

 
}
