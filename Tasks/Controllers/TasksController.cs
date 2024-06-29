using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
using Tasks.Services;
using AutoMapper;

namespace Tasks.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksItemService _tasksItemService;

        private readonly IMapper _mapper;


        public TasksController(ITasksItemService tasksItemService, IMapper mapper) 
        { 
           _tasksItemService = tasksItemService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _tasksItemService.GetIncompleteItemsAsync();

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

            var successful = await _tasksItemService.AddItemsAsync(tasksDTO);

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

            var successful = await _tasksItemService.MarkDoneAsync(id);

            if (!successful)
            {
                return BadRequest("Could not mark Item as done.");
            }
            return RedirectToAction("Index");
        }
    }
}
