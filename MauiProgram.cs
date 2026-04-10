using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using GesterTask.DataAcces;
using GesterTask.ViewModels;
using GesterTask.Views;

namespace GesterTask;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register EF Core InMemory database
        builder.Services.AddDbContext<TaskDbContext>(options =>
            options.UseInMemoryDatabase("TaskManagerDB"));

        // Register ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<TasksViewModel>();
        builder.Services.AddTransient<TaskDetailViewModel>();

        // Register AppShell and Pages
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<TasksPage>();
        builder.Services.AddTransient<TaskDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Ensure database is created and seeded
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
            dbContext.Database.EnsureCreated();
        }

        // Register navigation routes
        Routing.RegisterRoute("taskdetail", typeof(TaskDetailPage));

        return app;
    }
}
