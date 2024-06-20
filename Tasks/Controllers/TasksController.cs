using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tasks.Models;
using Tasks.Services;

namespace Tasks.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksItemService _tasksItemService;

        public TasksController(ITasksItemService tasksItemService) 
        { 
           _tasksItemService = tasksItemService;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _tasksItemService.GetIncompleteItemsAsync();

            var model = new TasksViewModel { Items = items };
            return View(model);
        }

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddItem(TasksModel tasksModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await _tasksItemService.AddItemsAsync(tasksModel);

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
