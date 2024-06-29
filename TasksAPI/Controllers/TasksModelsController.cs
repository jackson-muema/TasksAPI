using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksModelsController : ControllerBase
    {
        private readonly TasksContext _context;

        public TasksModelsController(TasksContext context)
        {
            _context = context;
        }

        // GET: api/TasksModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksDTO>>> GetTaskItems()
        {
            return await _context.TaskItems
                //Project each TaskItem entity into a TasksItem DTO
                .Select(x => TasksToDTO(x))
                .ToListAsync();
        }

        // GET: api/TasksModels/5  
        // <snippet_GetByID>
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksDTO>> GetTasksModel(int id)
        {
            var tasksModel = await _context.TaskItems.FindAsync(id);

            if (tasksModel == null)
            {
                return NotFound();
            }

            return TasksToDTO(tasksModel);
        }


        // PUT: api/TasksModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksModel(int id, TasksDTO tasksDTO)
        {
            if (id != tasksDTO.Id)
            {
                return BadRequest();
            }
            var tasksModel = await _context.TaskItems.FindAsync(id);
            if (tasksModel == null)
            {
                return NotFound();
            }

           tasksModel.Name = tasksDTO.Name;

            tasksModel.IsComplete = tasksDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when(!TasksModelExists(id))
            {
               
                {
                    return NotFound();
                }
              
            }

            return NoContent();
        }

        // POST: api/TasksModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasksDTO>> PostTasksModel(TasksDTO tasksDTO)
        {
            var tasksModel = new TasksModel
            {
                IsComplete = tasksDTO.IsComplete,
                Name = tasksDTO.Name,
            };

            _context.TaskItems.Add(tasksModel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTasksModel", new { id = tasksModel.Id }, tasksModel);
            return CreatedAtAction(nameof(PostTasksModel), new { id = tasksModel.Id }, TasksToDTO (tasksModel));
        }

        // DELETE: api/TasksModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksModel(int id)
        {
            var tasksModel = await _context.TaskItems.FindAsync(id);
            if (tasksModel == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(tasksModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksModelExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }

        //Create an helper method to convert the TasksModel in to a DTO
        private static TasksDTO TasksToDTO(TasksModel tasksModel) =>
            new TasksDTO
            {
                Id = tasksModel.Id,

                Name = tasksModel.Name,

                IsComplete = tasksModel.IsComplete,
            };
    }
}
