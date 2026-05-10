using MostWanted.ViewsModels;
namespace MostWanted
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public const double FontSize = 22;

        public MainPage(WantedPersonListViewModel wantedPersonListViewModel)
        {
            InitializeComponent();
            BindingContext = wantedPersonListViewModel;
            }


            //    public partial class MainPage : ContentPage
            //    {
            //    public const double FontSize = 22;
            //    private WantedPersonOnlineViewModel _vm;

            //        public MainPage()
            //        {
            //            InitializeComponent();
            //            _vm = new WantedPersonOnlineViewModel();
            //            BindingContext = _vm;
            //        }

            //        protected override async void OnAppearing()
            //        {
            //            base.OnAppearing();
            //            await _vm.LoadWantedPersonsAsync();
            //        }









            //    private async Task OnEnterClicked(object? sender, EventArgs e)
            //    {


            //        await Navigation.PushAsync(new LayoutExample());
            //    }
            //}


        public class GlobalFontSizeExtention : IMarkupExtension 
    {
        public object ProvideValue(IServiceProvider serviceProvider) {

            return MainPage.FontSize;
        }
    
    }
}}
