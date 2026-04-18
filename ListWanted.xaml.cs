using MostWanted.ViewsModels;
using MostWanted.Services;
using MostWanted.Model;
namespace MostWanted;

using System.Diagnostics;

public partial class ListWanted : ContentPage
{
	public ListWanted(WantedPersonListViewModel wantedPersonListViewModel)
	{
		InitializeComponent();
        Appearing += ListWanted_Appearing;

        BindingContext = wantedPersonListViewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is WantedPersonListViewModel vm)
        {
            await vm.GetWantedPersonListCommand.ExecuteAsync(null);
        }
    }



    private void ListWanted_Appearing(object? sender, EventArgs e)
    {
        GetWantedPerson();

    }

    private void GetWantedPerson()
    {
        try
        {
            // Ensure we have a writable DB path in the app data directory
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "wantedPerson.db");
            var wantedPersonService = new WantedPersonService(dbPath);
            //   var wantedPersons = wantedPersonService.GetWantedPerson();
            // TODO: Update your UI with the fetched data (e.g., bind to a ListView)
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching wanted persons: {ex}");
        }
    }
}