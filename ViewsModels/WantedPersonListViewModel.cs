using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted.Model;
using MostWanted.Services;
using MostWanted.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MostWanted.ViewsModels
{
    public partial class WantedPersonListViewModel : BaseViewModel
    {
        public ObservableCollection<WantedPerson> WantedPersons { get; private set; } = new();

        [ObservableProperty]
        
        private  int currentIndex;

        public WantedPersonListViewModel()
        {
            Title = "Wanted List";
            //  GetWantedPerson().Wait();


            GetWantedPersonListCommand.Execute(null);

            Application.Current.Dispatcher.StartTimer(TimeSpan.FromSeconds(12), () =>
            {
                if (WantedPersons.Count == 0) return true;

                CurrentIndex = (CurrentIndex + 1) % WantedPersons.Count;
                return true; // keep repeating
            });
        }

        
       
        [ObservableProperty]
        bool isRefreshing;


        [ObservableProperty]
        string name;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string type;

        [ObservableProperty]
        string imagePath; // <-- this is the path to the image


        //        [RelayCommand]
        //        async Task GetWantedPersonList()
        //        {

        //            Debug.WriteLine($"AddPage get command ");
        //            if (IsLoading) return;

        //            try
        //            {
        ////await Shell.Current.DisplayAlertAsync("Error", "Fail to retrive list os wanted person.", "OK:");
        //                IsLoading = true;

        //                if (WantedPersons.Any()) WantedPersons.Clear();
        //                var wantedPersons = App.WantedPersonService.GetWantedPersons();
        //                if (wantedPersons == null)
        //                {
        //                    Debug.WriteLine($"Unable to Get Wanted Person: ");
        //                    await Shell.Current.DisplayAlertAsync("Error", "Fail to retrive list os wanted person.", "OK:");
        //                }



        //                foreach (var wantedPerson in wantedPersons)

        //                {
        //                    WantedPersons.Add(wantedPerson);
        //                    // Storage Options
        //                    //string fileName = "mostWantedList.json";
        //                    //var serializedList=JsonSerializer.Serialize(wantedPersons);
        //                    //File.WriteAllText(fileName,serializedList); //This will store into the file system

        //                    //var rawText = File.ReadAllText(fileName);
        //                    //var wanterPersonsFromText= JsonSerializer.Deserialize<List<WantedPerson>>(rawText); //Getting the value from the json file
        //                    //string path = FileSystem.AppDataDirectory;
        //                    //string folder=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Debug.WriteLine($"Unable to Get Wanted Person: {ex.Message}");
        //                await Shell.Current.DisplayAlertAsync("Error", "Fail to retrive list os wanted person.", "OK:");


        //            }
        //            finally
        //            {
        //                IsLoading = false;
        //                IsRefreshing = false;

        //            }
        //        }

        [RelayCommand]
        async Task GetWantedPersonList()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;

                WantedPersons.Clear();

                var wantedPersons = App.WantedPersonService.GetWantedPersons();

                if (wantedPersons == null || !wantedPersons.Any())
                {
                    await Shell.Current.DisplayAlertAsync("Info", "No records found", "OK");
                    return;
                }

                foreach (var person in wantedPersons)
                {
                    WantedPersons.Add(person);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                await Shell.Current.DisplayAlertAsync("Error", "Failed to load data", "OK");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }


        //[RelayCommand]
        //async Task GetWantedPerson()
        //{


        // //   if (IsLoading) return;

        //    try
        //    {
        //        Debug.WriteLine($"Trying to get Wated Persons: ");
        //        IsLoading = true;

        //        if (WantedPersons.Any()) WantedPersons.Clear();

        //        var wantedPersons = App.WantedPersonService.GetWantedPerson();

        //        foreach (var wantedPerson in wantedPersons)

        //            WantedPersons.Add(wantedPerson);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Unable to get Wated Persons: {ex.Message}");
        //    }
        //}

        [RelayCommand]
        async Task GetWantedPersonDetails(int Id)
        {

            //Debug.WriteLine($" Wanted Persons ID: {id}");
            if (Id == 0) return;
            await Shell.Current.GoToAsync($"{nameof(WantedPersonsDetailPage)}?Id={Id}", true);


        }

        [RelayCommand]

        async Task AddPerson()
        {

            //Debug.WriteLine("I lov e yyyyyyyoooooooooooo");
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Type))
            {
                await Shell.Current.DisplayAlertAsync("Invalid Data", "Please Insert Data", "OK");
                return;
            }
             var wantedPerson = new WantedPerson
            {
                Name = Name,
                Description = Description,
                Type = Type,
                ImagePath = ImagePath
             };
            App.WantedPersonService.AddPerson(wantedPerson);
            await Shell.Current.DisplayAlertAsync("Info", App.WantedPersonService.StatusMessage,"OK");
            await GetWantedPersonList();


            Name = string.Empty;
            Description = string.Empty;
            Type = string.Empty;

        }
        [RelayCommand]
        async Task DeletePerson(int id)
        {
            if (id == 0) return;

            bool confirm = await Shell.Current.DisplayAlertAsync("Confirm", "Delete this person?", "Yes", "No");

            if (!confirm) return;

            App.WantedPersonService.DeletePerson(id);

            await GetWantedPersonList();
        }

        [RelayCommand]
        async Task UpdatePerson(WantedPerson person)
        {
            if (person == null) return;

            await Shell.Current.DisplayAlertAsync("Update", $"Update {person.Name}", "OK");

            // Add update logic later
        }


    }
}