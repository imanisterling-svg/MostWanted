using CommunityToolkit.Mvvm.ComponentModel;
using MostWanted.Model;
using MostWanted.Views;
using MostWanted.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Diagnostics;

namespace MostWanted.ViewsModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class WantedPersonDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]

        WantedPerson wantedPerson;

        [ObservableProperty]
        int id;

    



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


