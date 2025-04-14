using crudPractica.DTOs.TaskDto;

namespace crudPractica.Services.Task
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetAllAsync(int page, int pageSize);
        Task<TaskItemDto?> GetByIdAsync(int id);
        Task<TaskItemDto> CreateAsync(CreateTaskItemDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(UpdateTaskItemDto dto);
        Task<bool> MarkAsCompletedAsync(int id);
    }
}
