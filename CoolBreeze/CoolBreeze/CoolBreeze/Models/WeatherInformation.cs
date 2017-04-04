using System;
using CoolBreeze.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBreeze
{
    public class WeatherInformation : ObservableBase
    {
        private int _id;
        public int Id
        {
            get { return this._id; }
            set { this.SetProperty(ref this._id, value); }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return this._displayName; }
            set { this.SetProperty(ref this._displayName, value); }
        }

        private int _temperature;
        public int Temperature
        {
            get { return this._temperature; }
            set { this.SetProperty(ref this._temperature, value); }
        }

        private int _humidity;
        public int Humidity
        {
            get { return this._humidity; }
            set { this.SetProperty(ref this._humidity, value); }
        }

        private int _minTemperature;
        public int MinTemperature
        {
            get { return this._minTemperature; }
            set { this.SetProperty(ref this._minTemperature, value); }
        }

        private int _maxTemperature;
        public int MaxTemperature
        {
            get { return this._maxTemperature; }
            set { this.SetProperty(ref this._maxTemperature, value); }
        }

        private string _conditions;
        public string Conditions
        {
            get { return this._conditions; }
            set { this.SetProperty(ref this._conditions, value); }
        }

        private string _description;
        public string Description
        {
            get { return this._description; }
            set { this.SetProperty(ref this._description, value); }
        }

        private string _icon;
        public string Icon
        {
            get { return this._icon; }
            set { this.SetProperty(ref this._icon, value); }
        }

        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get { return this._timeStamp; }
            set { this.SetProperty(ref this._timeStamp, value); }
        }
    }
}