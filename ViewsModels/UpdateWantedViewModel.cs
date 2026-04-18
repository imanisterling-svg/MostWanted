using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted;
using MostWanted.Model;

public partial class UpdatePersonViewModel : ObservableObject
{
    [ObservableProperty]
    private WantedPerson person;

    public IRelayCommand SaveCommand { get; }

   public UpdatePersonViewModel(WantedPerson person)
    {
        Person = person;
        SaveCommand = new AsyncRelayCommand(SavePerson);
        CancelCommand = new AsyncRelayCommand(Cancel);
    }

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
