using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdApi.Context;
using ProdApi.Dtos;
using ProdApi.Models;
using ProdApi.Repository;

namespace ProdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _repository;
        private readonly IMapper _mapper;

        public TaskItemController(ITaskItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TaskItemDto>> GetAllTasks()
        {
            var tasks = await _repository.GetAllAsync();
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

            await _repository.AddAsync(taskItem);


            return CreatedAtAction(nameof(GetAllTasks), new { id = taskItem.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskItemDto task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound("The task record couldn't be found.");
            }

            existingTask.UpdateTaskItem(task.Title, task.Description, task.Status);

            await _repository.UpdateAsync(existingTask);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound("The task record couldn't be found.");
            }

            await _repository.DeleteAsync(existingTask);


            return NoContent();
        }




    }
}
