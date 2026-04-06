using MostWanted.ViewsModels;

namespace MostWanted.Views;

public partial class WantedPersonsDetailPage : ContentPage
{
 public WantedPersonsDetailPage(WantedPersonDetailsViewModel wantedPersonDetailsViewModel )
    {
        InitializeComponent();
        BindingContext = wantedPersonDetailsViewModel;
    }

  

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }
}