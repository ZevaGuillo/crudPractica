using crudPractica.Models;

namespace crudPractica.Repositories.Task
{
    public interface ItaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync(int page, int pageSize);
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
    }
}
