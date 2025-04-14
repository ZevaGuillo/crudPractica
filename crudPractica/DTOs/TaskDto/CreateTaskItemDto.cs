namespace crudPractica.DTOs.TaskDto
{
    public class CreateTaskItemDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}
