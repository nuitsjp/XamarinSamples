using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XFStopwatch.Models;

namespace XFStopwatch.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IStopwatch _stopwatch;
        public TimeSpan ElapsedTime { get; }
        public StopwatchStatus Status { get; }
        public ReadOnlyObservableCollection<LapTime> LapTimes { get; }
        public ICommand StartOrStopCommand { get; }
        public ICommand PauseOrResetCommand { get; }
        public ICommand NavigationDetailCommand { get; }

        public MainPageViewModel(IStopwatch stopwatch)
        {
            _stopwatch = stopwatch;
            ElapsedTime = _stopwatch.ElapsedTime;
            Status = _stopwatch.Status;
            LapTimes = new ReadOnlyObservableCollection<LapTime>(new ObservableCollection<LapTime>(_stopwatch.LapTimes));

            StartOrStopCommand = new Command(OnStartOrStopCommand);
            PauseOrResetCommand = new Command(OnPauseOrResetCommand, CanPauseOrResetCommandExecute);
        }

        private void OnStartOrStopCommand()
        {
            _stopwatch.Start();
        }

        private void OnPauseOrResetCommand()
        {
            
        }

        private bool CanPauseOrResetCommandExecute()
        {
            return true;
        }
    }
}
