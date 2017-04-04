using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CoolBreeze
{
    public partial class LocationPage : ContentPage
    {
        public LocationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (cityPicker.SelectedIndex < 0) cityPicker.SelectedIndex = 0;
            base.OnAppearing();
        }

        private void UseLocationToggled(object sender, ToggledEventArgs e)
        {
            App.ViewModel.NeedsRefresh = true;

            if (e.Value)
            {
                App.ViewModel.LocationType = Common.LocationType.Location;
            }
            else
            {
                App.ViewModel.LocationType = Common.LocationType.City;
            }
        }

        private void SelectedCityChanged(object sender, EventArgs e)
        {
            if (!App.ViewModel.IsBusy)
            {
                useLocationToggle.IsToggled = false;

                App.ViewModel.NeedsRefresh = true;
                App.ViewModel.LocationType = Common.LocationType.City;

                string selectedItem = (sender as Picker).Items[(sender as Picker).SelectedIndex];

                var cityName = selectedItem.Split('(').First().Trim();
                var countryCode = selectedItem.Split('(').Last().Replace(")", "").Trim();

                App.ViewModel.CityName = cityName;
                App.ViewModel.CountryCode = countryCode;
            }

            App.ViewModel.IsBusy = false;
        }
    }
}