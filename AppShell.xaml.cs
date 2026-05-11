using MostWanted.Views;


namespace MostWanted
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WantedPersonsDetailPage), typeof(WantedPersonsDetailPage));
            Routing.RegisterRoute(nameof(ReportSpottedPage), typeof(ReportSpottedPage));


            //NavigationPage LayoutExample = new NavigationPage(new LayoutExample());
        }
    }
}
