using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using XFStopwatch.Models;

namespace XFStopwatch.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly IStopwatch _stopwatch;
        public TimeSpan ElapsedTime { get; }

        public ReadOnlyObservableCollection<LapTime> LapTimes { get; }

        public StopwatchStatus Status { get; }

        public ICommand StartOrStopCommand { get; }

        public ICommand PauseOrResetCommand { get; }

        public ICommand NavigationDetailCommand { get; }

        public MainPageViewModel(IStopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }
    }
}
