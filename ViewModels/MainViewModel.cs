using System.Collections.ObjectModel;
using GesterTask.DataAcces;
using GesterTask.Models;

namespace GesterTask.ViewModels;

public class MainViewModel : BindingUtilObject
{
    private readonly TaskDbContext _dbContext;
    private int _pendingCount;
    private int _completedCount;

    public int PendingCount
    {
        get => _pendingCount;
        set => SetProperty(ref _pendingCount, value);
    }

    public int CompletedCount
    {
        get => _completedCount;
        set => SetProperty(ref _completedCount, value);
    }

    public MainViewModel(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
        LoadStats();
    }

    public void LoadStats()
    {
        var tasks = _dbContext.Tasks.ToList();
        PendingCount = tasks.Count(t => !t.IsCompleted);
        CompletedCount = tasks.Count(t => t.IsCompleted);
    }
}
