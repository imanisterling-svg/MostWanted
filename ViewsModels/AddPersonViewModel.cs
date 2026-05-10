using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted.Model;
using MostWanted.Services;
using MostWanted.ViewsModels;
using System.Diagnostics;

namespace MostWanted.ViewModels;

public partial class AddPersonViewModel : BaseViewModel
{
    private readonly WantedPersonServiceOnline _service;

    [ObservableProperty] private string name;
    [ObservableProperty] private string description;
    [ObservableProperty] private string type;
    [ObservableProperty] private string imagePath;

    public AddPersonViewModel(WantedPersonServiceOnline service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task Save()
    {

        Debug.WriteLine("Trying to save to Online Database");


        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Description) ||
            string.IsNullOrWhiteSpace(Type))
        {
            await Shell.Current.DisplayAlert("Invalid Data", "Please fill all fields", "OK");
            return;
        }

        var person = new WantedPerson
        {
            Name = Name,
            Description = Description,
            Type = Type,
            ImagePath = ImagePath
        };

        await _service.AddPersonAsync(person);

        await Shell.Current.DisplayAlert("Result", _service.StatusMessage, "OK");

        // clear form
        Name = string.Empty;
        Description = string.Empty;
        Type = string.Empty;
        ImagePath = string.Empty;
    }
}
