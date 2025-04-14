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

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TaskItemDto>> getAllAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItemDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
