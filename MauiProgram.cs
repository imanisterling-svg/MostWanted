using Microsoft.Extensions.Logging;
using MostWanted.Services;
using MostWanted.Views;
using MostWanted.ViewsModels;

namespace MostWanted
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
              .ConfigureFonts(fonts =>
              {
                  fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                  fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
              });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mostwanted.db");
            string spottedDbPath = Path.Combine(FileSystem.AppDataDirectory, "spotted.db");

            // Register local SQLite service
            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<WantedPersonService>(s, dbPath));

            // Register online service
            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<WantedPersonServiceOnline>(s, dbPath));

            // Register ViewModels
            builder.Services.AddTransient<WantedPersonListViewModel>();
            builder.Services.AddTransient<WantedPersonOnlineViewModel>();
            builder.Services.AddTransient<AddPersonViewModel>();

            // Register Views
            builder.Services.AddSingleton<ListWanted>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<WantedPersonsDetailPage>();
            builder.Services.AddTransient<WantedPersonsDetailPageOnline>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
