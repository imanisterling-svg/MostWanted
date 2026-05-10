using CommunityToolkit.Mvvm.ComponentModel;
using MostWanted.Model;
using MostWanted.Views;
using MostWanted.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.Input;

namespace MostWanted.ViewsModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class AddPersonViewModel : BaseViewModel, IQueryAttributable
    {

        private readonly SpottedService _spottedService;



        private readonly WantedPersonServiceOnline onlineService;


        // ensure non-null default to satisfy compiled bindings
        [ObservableProperty]
        WantedPerson wantedPerson = new WantedPerson();

        [ObservableProperty]
        int id;
        private WantedPerson selectedPerson;

        [RelayCommand]


       
        private async Task ReportSpotted()
        {
            var spottedService = new SpottedService(); // however you resolve this
            var page = new ReportSpottedPage(spottedService, WantedPerson ?? selectedPerson);

            await Shell.Current.Navigation.PushModalAsync(page);

            Debug.WriteLine("❌ I am right here");
        }




        //        private async Task ReportSpotted()
        //{
        //    // Use an action sheet (multiple choice) instead of DisplayAlertAsync
        //    string action = await Shell.Current.DisplayActionSheetAsync(
        //        "Report Spotted",
        //        "Cancel",
        //        null,
        //        "Take Photo",
        //        "Record Video"
        //    );

        //    switch (action)
        //    {
        //        case "Take Photo":
        //            await CapturePhoto();
        //            break;

        //        case "Record Video":
        //            await CaptureVideo();
        //            break;
        //    }
        //}

        private async Task CapturePhoto()
        {
            await Shell.Current.DisplayAlertAsync("Info", "CapturePhoto not implemented.", "OK");
        }

        private async Task CaptureVideo()
        {
            await Shell.Current.DisplayAlertAsync("Info", "CaptureVideo not implemented.", "OK");
        }


        // Implement as async void to satisfy the IQueryAttributable void signature
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
             Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
           // Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
            // Use the generated property / Id instead of the backing field


            var person = App.WantedPersonService.GetWantedPersonInfo(Id);




            if (person == null)
            {
                Debug.WriteLine("❌ PERSON NOT FOUND");
            }
            else
            {
                Debug.WriteLine($"✅ FOUND: {person.Name}");
            }
            WantedPerson = person;
        }
    }
}



//using MostWanted.Services;
//using CommunityToolkit.Mvvm.ComponentModel;
//using System.Web;


//namespace MostWanted.ViewsModels
//{
//    [QueryProperty(nameof(id), nameof(id))]
//    public partial class WantedPersonDetailsViewModel : BaseViewModel, IQueryAttributable
//    {


//        [ObservableProperty]
//        int id;



//        public void ApplyQueryAttributes(IDictionary<string, object> query)
//        {
//            if (query.ContainsKey("Id"))
//            {
//                id = Convert.ToInt32(query["Id"]);
//                WantedPerson = App.WantedPerson.GetWantedPerson(Id);
//            }
//        }
//}




////using CommunityToolkit.Mvvm.ComponentModel;
////using MostWanted.Model;
////using MostWanted.Services;

////namespace MostWanted.ViewsModels
////{
////    [QueryProperty(nameof(Id), "Id")]
////    public partial class WantedPersonDetailsViewModel : BaseViewModel, IQueryAttributable
////    {
////        private readonly WantedPersonService _service;

////        public WantedPersonDetailsViewModel(WantedPersonService service)
////        {
////            _service = service;
////        }

////        [ObservableProperty]
////        WantedPerson? wantedPerson;

////        [ObservableProperty]
////        int id;

////        public void ApplyQueryAttributes(IDictionary<string, object> query)
////        {
////            if (query.ContainsKey("Id"))
////            {
////                Id = Convert.ToInt32(query["Id"]);
////                WantedPerson = _service.GetWantedPerson(Id);
////            }
////        }
////    }
////}











