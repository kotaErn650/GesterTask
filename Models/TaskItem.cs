namespace GesterTask.Models;

public record TaskItem
{
    public int Id { get; init; }
    public string Titulo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public int CategoryId { get; set; }
}
