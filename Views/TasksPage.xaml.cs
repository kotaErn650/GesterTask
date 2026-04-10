using GesterTask.ViewModels;
using GesterTask.Handlers;

namespace GesterTask.Views;

public partial class TasksPage : ContentPage
{
    private readonly TasksViewModel _viewModel;

    public TasksPage(TasksViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadTasks();

        if (Shell.GetSearchHandler(this) is TaskSearchHandler handler)
            handler.OnQueryTextChanged = text => _viewModel.SearchText = text;
    }
}
