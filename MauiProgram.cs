using Microsoft.Extensions.Logging;
using MostWanted.Services;
using MostWanted.ViewsModels;
using MostWanted.Views;

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

            // string dbPath = Path.Combine(FileSystem.AppDataDirectory, "wantedPerson.db");

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mostwanted.db");
     
            //builder.Services.AddSingleton(new WantedPersonService(dbPath));

            builder.Services.AddSingleton(s=>ActivatorUtilities.CreateInstance<WantedPersonService>(s,dbPath));

      
            




            builder.Services.AddTransient<WantedPersonListViewModel>();

            builder.Services.AddTransient<WantedPersonDetailsViewModel>();

           
builder.Services.AddSingleton<ListWanted>();



            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<WantedPersonsDetailPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }

    // DatabaseService is provided by MostWanted.Services.DatabaseService
}
