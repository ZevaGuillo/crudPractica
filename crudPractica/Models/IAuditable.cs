namespace crudPractica.Models
{
    /// <summary>
    /// Define los campos básicos de auditoría para ser usados por cualquier entidad que quiera ser auditada.
    /// </summary>
    public interface IAuditable
    {
        DateTime CreatedAt { get; set; } // Fecha de creación
        DateTime UpdatedAt { get; set; } // Fecha de última modificación
    }
}
