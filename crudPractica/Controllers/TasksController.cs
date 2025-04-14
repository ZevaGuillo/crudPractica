using crudPractica.DTOs.TaskDto;
using crudPractica.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace crudPractica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest("Parámetros de paginación inválidos.");

            var tasks = await _taskService.GetAllAsync(page, pageSize);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            return (task != null) ? Ok(task) : NotFound($"Tarea con ID {id} no encontrada.");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemDto dto)
        {
            var created = await _taskService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound($"Tarea con ID {id} no encontrada.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskItemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la ruta no coincide con el cuerpo.");

            var updated = await _taskService.UpdateAsync(dto);
            if (!updated) return NotFound($"Tarea con ID {id} no encontrada.");

            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var success = await _taskService.MarkAsCompletedAsync(id);
            return success ? NoContent() : NotFound($"Tarea con ID {id} no encontrada.");
        }
    }
}
