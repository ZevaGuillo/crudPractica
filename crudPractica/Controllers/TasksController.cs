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
            if (task == null)
                return NotFound($"Tarea con ID {id} no encontrada.");

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("El título es obligatorio.");

            try
            {
                var created = await _taskService.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"Tarea con ID {id} no encontrada.");

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskItemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID de la ruta no coincide con el cuerpo.");

            try
            {
                var updated = await _taskService.UpdateAsync(dto);
                if (!updated) return NotFound($"Tarea con ID {id} no encontrada.");

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            var success = await _taskService.MarkAsCompletedAsync(id);
            if (!success)
                return NotFound($"Tarea con ID {id} no encontrada.");

            return NoContent();
        }
    }
}
