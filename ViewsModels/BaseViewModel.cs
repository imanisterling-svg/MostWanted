using CommunityToolkit.Mvvm.ComponentModel;

using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MostWanted.ViewsModels
{


    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        string title;



        [ObservableProperty]

        [NotifyPropertyChangedFor(nameof(IsNotLoading))]

        bool isLoading;

        public bool IsNotLoading => !isLoading;

        internal void OnAppearing()
        {
            throw new NotImplementedException();
        }
    }




}




