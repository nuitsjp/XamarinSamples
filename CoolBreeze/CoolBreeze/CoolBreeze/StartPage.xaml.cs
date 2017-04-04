using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CoolBreeze
{
    public partial class StartPage : TabbedPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            InitializeAppAsync();
            base.OnAppearing();
        }

        private void InitializeAppAsync()
        {
            if (App.ViewModel == null) App.ViewModel = new MainViewModel();
        }

        private async void ViewForecastClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForecastPage());
        }
    }
}