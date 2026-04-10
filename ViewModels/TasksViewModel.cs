using System.Collections.ObjectModel;
using System.Windows.Input;
using GesterTask.DataAcces;
using GesterTask.Models;

namespace GesterTask.ViewModels;

public class TasksViewModel : BindingUtilObject
{
    private readonly TaskDbContext _dbContext;
    private string _searchText = string.Empty;
    private ObservableCollection<TaskItemViewModel> _tasks = new();

    public ObservableCollection<TaskItemViewModel> Tasks
    {
        get => _tasks;
        set => SetProperty(ref _tasks, value);
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
                ApplyFilter();
        }
    }

    public ICommand NavigateToDetailCommand { get; }

    public TasksViewModel(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
        LoadTasks();

        NavigateToDetailCommand = new Command<TaskItemViewModel>(async vm =>
        {
            if (vm is not null)
                await Shell.Current.GoToAsync($"taskdetail?id={vm.Id}");
        });
    }

    public void LoadTasks()
    {
        var all = _dbContext.Tasks.ToList()
                      .Select(t => new TaskItemViewModel(t))
                      .ToList();

        Tasks = new ObservableCollection<TaskItemViewModel>(all);
    }

    private void ApplyFilter()
    {
        var all = _dbContext.Tasks.ToList();

        var filtered = string.IsNullOrWhiteSpace(_searchText)
            ? all
            : all.Where(t => t.Titulo.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();

        Tasks = new ObservableCollection<TaskItemViewModel>(
            filtered.Select(t => new TaskItemViewModel(t)));
    }
}
