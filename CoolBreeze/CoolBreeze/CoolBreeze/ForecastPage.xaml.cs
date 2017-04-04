using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CoolBreeze
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastPage : ContentPage
    {
        public ForecastPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            this.BindingContext = App.ViewModel;
            App.ViewModel.RefreshForecastAsync();
            base.OnAppearing();
        }
    }
}
