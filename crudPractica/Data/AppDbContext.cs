using crudPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace crudPractica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<Category> Categories => Set<Category>();

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                    .Where(e => e.Entity is TaskItem && (e.State == EntityState.Added || e.State == EntityState.Modified )))
            {
                DateTime now = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                    ((TaskItem)entry.Entity).CreatedAt = now;
                ((TaskItem)entry.Entity).UpdateAt = now;
            }
            return base.SaveChanges();
        }
    }
}
