using MostWanted.Model;
using MostWanted.Services;
using MostWanted.ViewModels;

namespace MostWanted.Views;

public partial class ReportSpottedPage : ContentPage
{
    public ReportSpottedPage(SpottedService service, WantedPerson person)
    {
        InitializeComponent();
        BindingContext = new ReportSpottedViewModel(service)
        {
            WantedPerson = person
        };
    }
}
