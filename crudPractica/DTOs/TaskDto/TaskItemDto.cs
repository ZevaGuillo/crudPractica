namespace crudPractica.DTOs.TaskDto
{
    public class TaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsCompleted { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;    
    }
}
