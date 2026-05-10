using MostWanted.ViewsModels;

namespace MostWanted.Views;

public partial class WantedPersonsDetailPageOnline : ContentPage
{
    public WantedPersonsDetailPageOnline(WantedPersonOnlineViewModel wantedPersonOnlineViewModel)
    {
        InitializeComponent();
        BindingContext = wantedPersonOnlineViewModel;
    }



    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }
}