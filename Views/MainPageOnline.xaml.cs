using MostWanted.ViewsModels;
namespace MostWanted
{
    public partial class MainPageOnline : ContentPage
    {
        int count = 0;
        public const double FontSize = 22;

 
            private WantedPersonOnlineViewModel _vm;

            public MainPageOnline(WantedPersonOnlineViewModel vm)
            {
                InitializeComponent();
                _vm = vm;
                BindingContext = _vm;
            }

            protected override async void OnAppearing()
            {
                base.OnAppearing();
                await _vm.GetWantedPersonListCommand.ExecuteAsync(null);
            }
        



        public class GlobalFontSizeExtention : IMarkupExtension
        {
            public object ProvideValue(IServiceProvider serviceProvider)
            {

                return MainPage.FontSize;
            }

        }
    }
}
