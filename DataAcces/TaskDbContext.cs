using Microsoft.EntityFrameworkCore;
using GesterTask.Models;

namespace GesterTask.DataAcces;

public class TaskDbContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Nombre = "Urgente" },
            new Category { Id = 2, Nombre = "Trabajo" },
            new Category { Id = 3, Nombre = "Personal" }
        );

        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem { Id = 1, Titulo = "Revisar correos importantes", Descripcion = "Leer y responder todos los correos pendientes del día.", IsCompleted = false, CategoryId = 1 },
            new TaskItem { Id = 2, Titulo = "Preparar informe mensual", Descripcion = "Elaborar el informe de rendimiento del mes de marzo.", IsCompleted = false, CategoryId = 2 },
            new TaskItem { Id = 3, Titulo = "Llamar al médico", Descripcion = "Agendar cita de revisión anual con el médico de cabecera.", IsCompleted = true, CategoryId = 3 },
            new TaskItem { Id = 4, Titulo = "Reunión con el equipo", Descripcion = "Standup diario con el equipo de desarrollo a las 10:00 AM.", IsCompleted = false, CategoryId = 2 },
            new TaskItem { Id = 5, Titulo = "Comprar víveres", Descripcion = "Ir al supermercado: leche, frutas, verduras y pan.", IsCompleted = true, CategoryId = 3 }
        );
    }
}
