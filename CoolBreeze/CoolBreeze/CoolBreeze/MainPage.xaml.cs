using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CoolBreeze
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (App.ViewModel == null) App.ViewModel = new MainViewModel();

            this.BindingContext = App.ViewModel;

            if (App.ViewModel.NeedsRefresh) App.ViewModel.RefreshCurrentConditionsAsync();

            if (cityPicker.SelectedIndex < 0) cityPicker.SelectedIndex = 0;

            base.OnAppearing();
        }

        private void SelectedCityChanged(object sender, EventArgs e)
        {
            if (!App.ViewModel.IsBusy)
            {
                App.ViewModel.NeedsRefresh = true;
                App.ViewModel.LocationType = Common.LocationType.City;

                string selectedItem = (sender as Picker).Items[(sender as Picker).SelectedIndex];

                var cityName = selectedItem.Split('(').First().Trim();
                var countryCode = selectedItem.Split('(').Last().Replace(")", "").Trim();

                App.ViewModel.CityName = cityName;
                App.ViewModel.CountryCode = countryCode;

                App.ViewModel.RefreshCurrentConditionsAsync();
            }
        }
    }
}