namespace crudPractica.DTOs.TaskDto
{
    public class UpdateTaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
    }
}
