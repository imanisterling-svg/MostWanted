using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted;
using MostWanted.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

public partial class UpdatePersonViewModel : ObservableObject
{
    [ObservableProperty]
    private WantedPerson person;

    public IRelayCommand SaveCommand { get; }

    public UpdatePersonViewModel(WantedPerson person)
    {


        Person = person;


        // Normalize Person.Type to match OffenceTypes
        if (!string.IsNullOrEmpty(Person.Type))
        {
            var match = OffenceTypes.FirstOrDefault(x =>
                string.Equals(x, Person.Type, StringComparison.OrdinalIgnoreCase));
            if (match != null)
            {
                Debug.WriteLine($"Offence Type: {Person.Type}");
                Person.Type = match;
            }
            else
            {
                Debug.WriteLine($"Offence Type No Match : {Person.Type}");
            }



        }



        SaveCommand = new AsyncRelayCommand(SavePerson);
        CancelCommand = new AsyncRelayCommand(Cancel);
    }

     [ObservableProperty]

        string type;

    public ObservableCollection<string> OffenceTypes { get; } =
    new ObservableCollection<string>
    {
        "Theft",
        "Fraud",
        "Assault",
        "Drug Trafficking",
        "Murder",
        "Cybercrime"
    };

    private async Task SavePerson()
    {
        App.WantedPersonService.UpdatePerson(Person);
        await Shell.Current.DisplayAlertAsync("Info", App.WantedPersonService.StatusMessage, "OK");

        // Close modal
        await Application.Current.MainPage.Navigation.PopModalAsync();
    }


    public IAsyncRelayCommand CancelCommand { get; }

  

    private async Task Cancel()
    {
        await Application.Current.MainPage.Navigation.PopModalAsync();
    }






}
