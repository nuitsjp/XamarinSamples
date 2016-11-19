using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OnBackButtonPressed.Annotations;

namespace OnBackButtonPressed.ViewModels
{
    public class SecondPageViewModel : IConfirmGoBack, INotifyPropertyChanged
    {
        private bool _isProcessing;

        public bool IsProcessing
        {
            get { return _isProcessing; }
            set
            {
                _isProcessing = value;
                OnPropertyChanged();
            }
        }
        public bool IsEnabled { get; set; }
        public bool CanGoBack()
        {
            return IsEnabled;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> CanGoBackAsync()
        {
            IsProcessing = true;
            try
            {
                await Task.Delay(2000);
                return true;
            }
            finally
            {
                IsProcessing = false;
            }
        }
    }
}