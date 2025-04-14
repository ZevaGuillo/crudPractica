using crudPractica.DTOs.CategoryDto;
using crudPractica.Models;
using crudPractica.Repositories.CategoryRepo;

namespace crudPractica.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            var created = await _repository.CreateAsync(category);

            _logger.LogInformation("Created category with ID {Id}", created.Id);

            return new CategoryDto
            {
                Id = created.Id,
                Name = created.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (deleted)
                _logger.LogWarning("Deleted category with ID {Id}", id);
            else
                _logger.LogWarning("Attempted to delete non-existent category with ID {Id}", id);

            return deleted;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
        {
            var category = new Category { Id = dto.Id, Name = dto.Name };
            var updated = await _repository.UpdateAsync(category);

            if (updated)
                _logger.LogInformation("Updated category with ID {Id}", dto.Id);
            else
                _logger.LogWarning("Attempted to update non-existent category with ID {Id}", dto.Id);

            return updated;
        }
    }
}
