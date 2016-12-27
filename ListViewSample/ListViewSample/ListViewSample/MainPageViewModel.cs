using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewSample
{
    public class MainPageViewModel
    {
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        public Command AddItemCommand => new Command(() =>
        {
            Items.Add($"Item{Items.Count}");
        });

        public MainPageViewModel()
        {
            for (int i = 0; i < 5; i++)
            {
                Items.Add($"Item{i}");
            }
        }
    }
}
