using crudPractica.Data;
using crudPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace crudPractica.Repositories.Task
{
    public class TaskRepository : ItaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Tasks
                .Include(t => t.Category)
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            var existing = await _context.Tasks.FindAsync(task.Id);
            if (existing == null) return false;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;
            existing.CategoryId = task.CategoryId;
            existing.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
