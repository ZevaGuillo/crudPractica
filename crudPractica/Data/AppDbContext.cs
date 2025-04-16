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
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Sobrescribe SaveChangesAsync para aplicar las reglas de auditoría
        /// en todas las entidades que implementen IAuditable.
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Busca todas las entidades en seguimiento que implementen IAuditable
            // y que estén en estado Added (nuevas) o Modified (editadas)
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            DateTime now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                var auditable = (IAuditable)entry.Entity;

                // Si es una entidad nueva, asignamos también la fecha de creación
                if (entry.State == EntityState.Added)
                    auditable.CreatedAt = now;

                // En ambos casos se actualiza UpdatedAt
                auditable.UpdatedAt = now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
