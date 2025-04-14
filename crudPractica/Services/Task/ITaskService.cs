using crudPractica.DTOs.TaskDto;

namespace crudPractica.Services.Task
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> getAllAsync(int page, int pageSize);
        Task<TaskItemDto?> GetByIdAsync(int id);
        Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
