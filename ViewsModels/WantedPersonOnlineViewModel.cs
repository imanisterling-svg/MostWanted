using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted.Model;
using MostWanted.Services;
using MostWanted.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MostWanted.ViewsModels
{
    public partial class WantedPersonOnlineViewModel : BaseViewModel
    {
        private readonly WantedPersonServiceOnline _onlineService;
        private readonly WantedPersonService _offlineService;

        [ObservableProperty]
        private string title = "Wanted List";

        [ObservableProperty]
        private int currentIndex;

        public ObservableCollection<WantedPerson> WantedPersons { get; } = new();
        public bool IsRefreshing { get; private set; }

        public WantedPersonOnlineViewModel(
            WantedPersonServiceOnline onlineService,
            WantedPersonService offlineService)
        {
            _onlineService = onlineService;
            _offlineService = offlineService;
        }

        [RelayCommand]
        private async Task GetWantedPersonList()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                WantedPersons.Clear();

                var wantedPersons = await _onlineService.GetWantedPersonsAsync();

                if (wantedPersons == null || wantedPersons.Count == 0)
                {
                    await Shell.Current.DisplayAlert("Info", "No records found", "OK");
                    return;
                }

                foreach (var person in wantedPersons)
                    WantedPersons.Add(person);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to load data", "OK");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }





        [RelayCommand]
        private async Task GetWantedPersonDetails(int id)
        {
            var person = await _onlineService.GetWantedPersonInfoAsync(id);
                        

            if (person != null)
            {
                Debug.WriteLine($"Found Online: {person.Name}, {person.Description}");
                await Shell.Current.GoToAsync($"{nameof(WantedPersonsDetailPage)}?Id={person.Id}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("Info", "No person found", "OK");
            }
        }




















        //[RelayCommand]
        //private async Task GetWantedPersonDetails(int id)
        //{
        //    var person = await _onlineService.GetWantedPersonInfoAsync(id)
        //                 ?? _offlineService.GetWantedPersonInfo(id);

        //    if (person != null)
        //    {
        //        Debug.WriteLine($"Found: {person.Name}, {person.Description}");
        //        await Shell.Current.GoToAsync($"{nameof(WantedPersonsDetailPage)}?Id={person.Id}", true);
        //    }
        //    else
        //    {
        //        await Shell.Current.DisplayAlert("Info", "No person found", "OK");
        //    }
        //}
    }
}
