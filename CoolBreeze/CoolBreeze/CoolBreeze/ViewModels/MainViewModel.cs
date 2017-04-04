using CoolBreeze.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBreeze
{
    public class MainViewModel : ObservableBase
    {
        private Plugin.Geolocator.Abstractions.Position _location;
        public Plugin.Geolocator.Abstractions.Position Location
        {
            get { return this._location; }
            set { this.SetProperty(ref this._location, value); }
        }

        public MainViewModel()
        {
            this.IsBusy = true;
            this.NeedsRefresh = true;
            this.LocationType = LocationType.City;
            this.CityName = "Amsterdam";
            this.CountryCode = "HL";
            this.CurrentConditions = new WeatherInformation();
        }

        private LocationType _locationType;
        public LocationType LocationType
        {
            get { return this._locationType; }
            set { this.SetProperty(ref this._locationType, value); }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return this._postalCode; }
            set { this.SetProperty(ref this._postalCode, value); }
        }

        private string _cityName;
        public string CityName
        {
            get { return this._cityName; }
            set { this.SetProperty(ref this._cityName, value); }
        }

        private string _countryCode;
        public string CountryCode
        {
            get { return this._countryCode; }
            set { this.SetProperty(ref this._countryCode, value); }
        }

        private WeatherInformation _currentConditions;
        public WeatherInformation CurrentConditions
        {
            get { return this._currentConditions; }
            set { this.SetProperty(ref this._currentConditions, value); }
        }

        private bool _needsRefresh;
        public bool NeedsRefresh
        {
            get { return this._needsRefresh || string.IsNullOrEmpty(this._currentConditions.Conditions); }
            set { this.SetProperty(ref this._needsRefresh, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetProperty(ref this._isBusy, value); }
        }

        public async void RefreshCurrentConditionsAsync()
        {
            this.IsBusy = true;
            this.NeedsRefresh = false;

            WeatherInformation results = null;

            switch (this.LocationType)
            {
                case LocationType.Location:
                    if (this.Location == null) this.Location = await Helpers.LocationHelper.GetCurrentLocationAsync();
                    results = await Helpers.WeatherHelper.GetCurrentConditionsAsync(this.Location.Latitude, this.Location.Longitude);
                    break;

                case LocationType.City:
                    results = await Helpers.WeatherHelper.GetCurrentConditionsAsync(this.CityName, this.CountryCode);
                    break;
            }

            this.CurrentConditions.Conditions = results.Conditions;
            this.CurrentConditions.Description = results.Description;
            this.CurrentConditions.DisplayName = results.DisplayName;
            this.CurrentConditions.Icon = results.Icon;
            this.CurrentConditions.Id = results.Id;
            this.CurrentConditions.MaxTemperature = results.MaxTemperature;
            this.CurrentConditions.MinTemperature = results.MinTemperature;
            this.CurrentConditions.Temperature = results.Temperature;
            this.CurrentConditions.Humidity = results.Humidity;
            this.CurrentConditions.TimeStamp = results.TimeStamp.ToLocalTime();

            this.IsBusy = false;
        }

        public async void RefreshForecastAsync()
        {
            this.IsBusy = true;
            this.NeedsRefresh = false;

            List<WeatherInformation> results = null;

            this.Forecast.Clear();

            switch (this.LocationType)
            {
                case LocationType.Location:

                    if (this.Location == null) this.Location = await Helpers.LocationHelper.GetCurrentLocationAsync();
                    results = await Helpers.WeatherHelper.GetForecastAsync(this.Location.Latitude, this.Location.Longitude);
                    break;
                case LocationType.City:
                    results = await Helpers.WeatherHelper.GetForecastAsync(this.CityName, this.CountryCode);
                    break;
            }

            foreach (var result in results)
            {
                this.Forecast.Add(result);
            }

            this.IsBusy = false;
        }

        private ObservableCollection<WeatherInformation> _forecast;
        public ObservableCollection<WeatherInformation> Forecast
        {
            get { if (this._forecast == null) this._forecast = new ObservableCollection<WeatherInformation>(); return this._forecast; }
            set { this.SetProperty(ref this._forecast, value); }
        }
    }
}