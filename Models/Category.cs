namespace GesterTask.Models;

public record Category
{
    public int Id { get; init; }
    public string Nombre { get; set; } = string.Empty;
}
