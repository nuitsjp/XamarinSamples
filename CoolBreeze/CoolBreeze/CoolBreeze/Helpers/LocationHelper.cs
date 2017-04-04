using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBreeze.Helpers
{
    public static class LocationHelper
    {
        public async static Task<Plugin.Geolocator.Abstractions.Position> GetCurrentLocationAsync()
        {
            Plugin.Geolocator.Abstractions.Position location = new Plugin.Geolocator.Abstractions.Position(new Plugin.Geolocator.Abstractions.Position()
            {
                Latitude = 29.425700,
                Longitude = -98.486110,
            });

            var geolocator = Plugin.Geolocator.CrossGeolocator.Current;

            try
            {
                geolocator.DesiredAccuracy = 100;
                location = await geolocator.GetPositionAsync();
            }
            catch { }

            return location;
        }
    }
}