using System.Windows.Input;
using GesterTask.DataAcces;

namespace GesterTask.ViewModels;

public class TaskDetailViewModel : BindingUtilObject, IQueryAttributable
{
    private readonly TaskDbContext _dbContext;

    private int _id;
    private string _titulo = string.Empty;
    private string _descripcion = string.Empty;
    private bool _isCompleted;
    private string _categoryName = string.Empty;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Titulo
    {
        get => _titulo;
        set => SetProperty(ref _titulo, value);
    }

    public string Descripcion
    {
        get => _descripcion;
        set => SetProperty(ref _descripcion, value);
    }

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            if (SetProperty(ref _isCompleted, value))
                SaveChanges();
        }
    }

    public string CategoryName
    {
        get => _categoryName;
        set => SetProperty(ref _categoryName, value);
    }

    public ICommand GoBackCommand { get; }

    public TaskDetailViewModel(TaskDbContext dbContext)
    {
        _dbContext = dbContext;

        GoBackCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("id", out var idObj) && int.TryParse(idObj?.ToString(), out var id))
            LoadTask(id);
    }

    private void LoadTask(int id)
    {
        var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return;

        var category = _dbContext.Categories.FirstOrDefault(c => c.Id == task.CategoryId);

        Id = task.Id;
        Titulo = task.Titulo;
        Descripcion = task.Descripcion;
        _isCompleted = task.IsCompleted;
        OnPropertyChanged(nameof(IsCompleted));
        CategoryName = category?.Nombre ?? "Sin categoría";
    }

    private void SaveChanges()
    {
        var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == _id);
        if (task is null) return;

        task.IsCompleted = _isCompleted;
        _dbContext.SaveChanges();
    }
}
