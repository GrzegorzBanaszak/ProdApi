using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdApi.Context;
using ProdApi.Dtos;
using ProdApi.Models;

namespace ProdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly TaskDbContext _context;
        private readonly IMapper _mapper;

        public TaskItemController(TaskDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TaskItemDto>> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TaskItemDto>>(tasks));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDto task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            var taskItem = TaskItem.Create(task.Title, task.Description, task.Status);

            _context.Tasks.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllTasks), new { id = taskItem.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskItemDto task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound("The task record couldn't be found.");
            }

            existingTask.UpdateTaskItem(task.Title, task.Description, task.Status);

            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound("The task record couldn't be found.");
            }

            _context.Tasks.Remove(existingTask);
            await _context.SaveChangesAsync();


            return NoContent();
        }




    }
}
