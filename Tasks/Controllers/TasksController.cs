using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
using Tasks.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Tasks.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITasksItemService _tasksItemService;

        private readonly IMapper _mapper;

        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ITasksItemService tasksItemService, IMapper mapper, UserManager<IdentityUser> userManager) 
        { 
           _tasksItemService = tasksItemService;
            _mapper = mapper;
            _userManager = userManager; 
            
        }
        public async Task<IActionResult> Index()
        {
            var currentuser = await _userManager.GetUserAsync(User);

            if (currentuser == null)
            {
                return Challenge();
            }
            var items = await _tasksItemService.GetIncompleteItemsAsync(currentuser);

            var tasksViewModel = new TasksViewModel
            {
                Item = items
            };
            
            return View(tasksViewModel);
        }

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddItem(TasksDTO tasksDTO)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var currentuser = await _userManager.GetUserAsync(User);

            if (currentuser == null)
            {
                return Challenge();
            }
            var successful = await _tasksItemService.AddItemsAsync(tasksDTO, currentuser);

            if (!successful)
            {
                return BadRequest("Could not add new Task.");
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var currentuser = await _userManager.GetUserAsync(User);

            if (currentuser == null)
            {
                return Challenge();
            }
            var successful = await _tasksItemService.MarkDoneAsync(id, currentuser);

            if (!successful)
            {
                return BadRequest("Could not mark Item as done.");
            }
            return RedirectToAction("Index");
        }
    }
}
