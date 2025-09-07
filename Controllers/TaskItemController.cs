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
        private readonly ILogger<TaskItemController> _logger;

        public TaskItemController(ITaskItemRepository repository, IMapper mapper, ILogger<TaskItemController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<TaskItemDto>> GetAllTasks()
        {
            _logger.LogInformation("Pobieranie wszystkich zadań");
            var tasks = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TaskItemDto>>(tasks));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTaskById(Guid id)
        {
            _logger.LogInformation("Pobieranie zadania o Id: {Id}", id);
            var task = await _repository.GetByIdAsync(id);
            return Ok(_mapper.Map<TaskItemDto>(task));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDto dto)
        {
            _logger.LogInformation("Tworzenie nowego zadania: {Title}", dto.Title);
            if (dto == null)
            {
                return BadRequest("Task is null.");
            }

            var taskItem = TaskItem.Create(dto.Title, dto.Description, dto.Status);

            await _repository.AddAsync(taskItem);

            _logger.LogInformation("Utworzono zadanie o Id: {Id}", taskItem.Id);

            return CreatedAtAction(nameof(GetAllTasks), new { id = taskItem.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskItemDto task)
        {
            _logger.LogInformation("Aktualizacja zadania o Id: {Id}", id);
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

            _logger.LogInformation("Zaktualizowano zadanie o Id: {Id}", existingTask.Id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            _logger.LogInformation("Usuwanie zadania o Id: {Id}", id);
            var existingTask = await _repository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound("The task record couldn't be found.");
            }

            await _repository.DeleteAsync(existingTask);

            _logger.LogInformation("Usunięto zadanie o Id: {Id}", existingTask.Id);

            return NoContent();
        }




    }
}
