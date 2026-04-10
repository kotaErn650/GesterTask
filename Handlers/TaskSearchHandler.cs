namespace GesterTask.Handlers;

public class TaskSearchHandler : SearchHandler
{
    public Action<string>? OnQueryTextChanged { get; set; }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);
        OnQueryTextChanged?.Invoke(newValue ?? string.Empty);
    }
}
