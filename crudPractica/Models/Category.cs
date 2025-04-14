﻿namespace crudPractica.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
