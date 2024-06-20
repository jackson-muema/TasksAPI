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
        public async Task<ActionResult<IEnumerable<TasksModel>>> GetTaskItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // GET: api/TasksModels/5  
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksModel>> GetTasksModel(int id)
        {
            var tasksModel = await _context.TaskItems.FindAsync(id);

            if (tasksModel == null)
            {
                return NotFound();
            }

            return tasksModel;
        }


        // PUT: api/TasksModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksModel(int id, TasksModel tasksModel)
        {
            if (id != tasksModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(tasksModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TasksModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasksModel>> PostTasksModel(TasksModel tasksModel)
        {
            _context.TaskItems.Add(tasksModel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTasksModel", new { id = tasksModel.Id }, tasksModel);
            return CreatedAtAction(nameof(PostTasksModel), new { id = tasksModel.Id }, tasksModel);
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
    }
}
