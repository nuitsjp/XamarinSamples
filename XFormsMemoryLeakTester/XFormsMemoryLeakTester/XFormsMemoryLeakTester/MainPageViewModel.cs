using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using System.Reactive.Linq;

namespace XFormsMemoryLeakTester
{
    public class MainPageViewModel
    {
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }
        public ICommand GcCommand { get; } = new RelayCommand(() => GC.Collect());

        private IDisposable TimerSubscription { get; set; }

        const int MaxItemsCount = 10;

        int counter = 0;

        public MainPageViewModel()
        {
            StartCommand = new RelayCommand(() =>
            {
                if(TimerSubscription == null)
                {
                    TimerSubscription = Observable.Interval(TimeSpan.FromMilliseconds(20))
                        .Subscribe(_ =>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                Items.Add(new Item { Label = $"Label{++counter}" });
                                if (MaxItemsCount < Items.Count)
                                    Items.Remove(Items[0]);
                            });
                        });
                }
            });
            StopCommand = new RelayCommand(() =>
            {
                if (TimerSubscription != null)
                {
                    TimerSubscription.Dispose();
                    TimerSubscription = null;
                }
            });
        }
    }
}
