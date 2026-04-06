using Microsoft.Extensions.DependencyInjection;
using MostWanted.Services;

namespace MostWanted
{
    public partial class App : Application
    {


        public static WantedPersonService WantedPersonService { get; private set; }
    
        public App(WantedPersonService wantedPersonService)
        {
            InitializeComponent();
            MainPage = new AppShell();
            
            WantedPersonService = wantedPersonService;


        }

      


    }
}