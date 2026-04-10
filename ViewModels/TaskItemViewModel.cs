using GesterTask.Models;

namespace GesterTask.ViewModels;

public class TaskItemViewModel : BindingUtilObject
{
    private bool _isCompleted;

    public int Id { get; }
    public string Titulo { get; }
    public string Descripcion { get; }
    public int CategoryId { get; }

    public bool IsCompleted
    {
        get => _isCompleted;
        set => SetProperty(ref _isCompleted, value);
    }

    public TaskItemViewModel(TaskItem item)
    {
        Id = item.Id;
        Titulo = item.Titulo;
        Descripcion = item.Descripcion;
        CategoryId = item.CategoryId;
        _isCompleted = item.IsCompleted;
    }
}
