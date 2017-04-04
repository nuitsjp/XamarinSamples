using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoolBreeze.Helpers
{
    public class WeatherHelper
    {
        public async static Task<WeatherInformation> GetCurrentConditionsAsync(string cityName, string countryCode)
        {
            string url = $"http://traininglabservices.azurewebsites.net/api/weather/current/city?cityName={cityName}&countryCode={countryCode}&registrationCode={App.RegistrationCode}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherInformation>(response);
            return result;
        }

        public async static Task<WeatherInformation> GetCurrentConditionsAsync(double latitude, double longitude)
        {
            string url = $"http://traininglabservices.azurewebsites.net/api/weather/current?latitude={latitude}&longitude={longitude}&registrationCode={App.RegistrationCode}";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherInformation>(response);
            return result;
        }

        public async static Task<List<WeatherInformation>> GetForecastAsync(double latitude, double longitude)
        {
            string url = $"http://traininglabservices.azurewebsites.net/api/weather/forecast?latitude={latitude}&longitude={longitude}&registrationCode={App.RegistrationCode}";

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WeatherInformation>>(response);
            return result;
        }

        public async static Task<List<WeatherInformation>> GetForecastAsync(string cityName, string countryCode)
        {
            string url = $"http://traininglabservices.azurewebsites.net/api/weather/forecast/city?cityName={cityName}&countryCode={countryCode}&registrationCode={App.RegistrationCode}";

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WeatherInformation>>(response);
            return result;
        }
    }
}