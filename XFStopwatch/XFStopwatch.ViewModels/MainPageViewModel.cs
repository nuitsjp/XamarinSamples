﻿using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XFStopwatch.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value))
            {
                return false;
            }
            else
            {
                property = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
        }
        #endregion
    }
}
