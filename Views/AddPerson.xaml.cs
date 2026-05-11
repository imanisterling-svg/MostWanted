using Microsoft.Maui.Storage;
using MostWanted.Model;
using MostWanted.Services;
using MostWanted.ViewsModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Storage;
using System.IO;



using System.IO;

namespace MostWanted;

public partial class TestPage2 : ContentPage
{
	private int count;
    string selectedImagePath = null;
    public TestPage2(WantedPersonListViewModel wantedPersonListViewModel)
	{
		InitializeComponent();
       Appearing += TestPage2_Appearing;

        BindingContext = wantedPersonListViewModel;


    }


    private async void Button_SelectImage_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.PickPhotoAsync();

        if (result != null)
        {
            var path = result.FullPath;



            // Update ViewModel binding
            var vm = BindingContext as WantedPersonListViewModel;
            vm.ImagePath = path;
        }
    }

    private async void Button_TakePhoto_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.CapturePhotoAsync();
        if (result != null)
        {
            // Save to app folder
            var newFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
            using var stream = await result.OpenReadAsync();
            using var newStream = File.OpenWrite(newFile);
            await stream.CopyToAsync(newStream);

            // Update ViewModel binding
            var vm = BindingContext as WantedPersonListViewModel;
            vm.ImagePath = newFile;
        }
    }









    private void Button_Clicked(object sender, EventArgs e)
    {

        count++;
        LblCounter.Text = $"Thank For Clicking {count} times.";

        SemanticScreenReader.Announce(LblCounter.Text); // This will update the LblCounter area on the page

    }

    //private void Button_AddPerson(object sender, EventArgs e)
    //{


    //    string name = Name
    //    string description = Description.Text;
    //    string type = Type.Text;

    //    if (string.IsNullOrWhiteSpace(name) ||
    //      string.IsNullOrWhiteSpace(description) ||
    //      string.IsNullOrWhiteSpace(type))
    //    {
    //        DisplayAlertAsync("Invalid Data", "Please Insert Data", "OK");
    //        return;
    //    }

    //    var person = new WantedPerson
    //    {
    //        Name = name,
    //        Description = description,
    //        Type = type
    //    };

    //    App.WantedPersonService.AddPerson(person);

    //    DisplayAlertAsync("Success", "Person added", "OK");

    //    Name.Text = "";
    //    Description.Text = "";
    //    Type.Text = "";



    //    //  var wantedPerson = new WantedPerson
    //    //  {

    //    //      Name = "Imani",
    //    //      Description = "Love",
    //    //      Type = "Most Wanted"
    //    //  };

    //    //var wantedPersons = App.WantedPersonService.GetWantedPerson();





    //    //  App.WantedPersonService.AddPerson(wantedPerson);




    //}



    private void TestPage2_Appearing(object? sender, EventArgs e)
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