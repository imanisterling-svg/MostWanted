using Microsoft.Extensions.DependencyInjection;
using MostWanted.Services;


namespace MostWanted
{
    public partial class App : Application
    {


        public static WantedPersonService WantedPersonService { get; private set; }
        public static WantedPersonServiceOnline WantedPersonServiceOnline { get; internal set; }

        public App(WantedPersonService wantedPersonService)
        {

            WantedPersonService = wantedPersonService;




            InitializeComponent();
            MainPage = new AppShell();
            
            WantedPersonService = wantedPersonService;

            WantedPersonServiceOnline = WantedPersonServiceOnline;


            //FirebaseCloudMessaging.Initialize();

            //FirebaseCloudMessaging.OnNotificationReceived += (sender, e) =>
            //{
            //    Console.WriteLine($"Notification received: {e.Notification.Title}");
            //};

            //FirebaseCloudMessaging.OnTokenRefresh += (sender, e) =>
            //{
            //    Console.WriteLine($"FCM Token: {e.Token}");
            //    // TODO: send token to your backend
            //};



        }




    }
}