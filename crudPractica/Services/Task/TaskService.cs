using crudPractica.Data;
using crudPractica.DTOs.TaskDto;
using crudPractica.Models;
using crudPractica.Repositories.CategoryRepo;
using crudPractica.Repositories.Task;

namespace crudPractica.Services.Task
{
    public class TaskService : ITaskService
    {

        private readonly ItaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ItaskRepository taskRepository, ICategoryRepository categoryRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            
            if (category == null) throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");

            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CategoryId = dto.CategoryId
            };

            var created = await _taskRepository.CreateAsync(task);
            _logger.LogInformation("Created task with ID {id}", created.Id);

            return new TaskItemDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                IsCompleted = created.IsCompleted,
                CategoryId = created.CategoryId,
                CategoryName = category.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _taskRepository.DeleteAsync(id);
            if (result)
            {
                _logger.LogWarning("Deleted task with ID {Id}", id);
            }
            return result;
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync(int page, int pageSize)
        {
            var tasks = await _taskRepository.GetAllAsync(page, pageSize);

            return tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CategoryId = t.CategoryId,
                CategoryName = t.Category?.Name ?? ""
            });
        }

        public async Task<TaskItemDto?> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskItemDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CategoryId = task.CategoryId,
                CategoryName = task.Category?.Name ?? ""
            };
        }

        public async Task<bool> MarkAsCompletedAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            task.IsCompleted = true;
            await _taskRepository.UpdateAsync(task);
            _logger.LogInformation("Marked task ID {Id} as completed", task.Id);
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateTaskItemDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(dto.Id);
            if (task == null) return false;

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null) throw new ArgumentException("Categoría no válida.");

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;
            task.CategoryId = dto.CategoryId;

            await _taskRepository.UpdateAsync(task);
            _logger.LogInformation("Updated task ID {Id}", task.Id);
            return true;
        }
    }
}
