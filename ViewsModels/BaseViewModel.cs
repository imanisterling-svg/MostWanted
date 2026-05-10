using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MostWanted.Model;
using MostWanted.ViewsModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace MostWanted.ViewsModels
{


    public partial class BaseViewModel : ObservableObject
    {
    private readonly SpottedService _service;


  

        [ObservableProperty]
        string title;



        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(IsNotLoading))]

        bool isLoading;

        public bool IsNotLoading => !isLoading;
    public WantedPerson WantedPerson { get; set; }

        internal void OnAppearing()
        {
            throw new NotImplementedException();
        }
    }




}




