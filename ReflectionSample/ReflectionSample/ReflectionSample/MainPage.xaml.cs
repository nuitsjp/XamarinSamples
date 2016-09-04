using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReflectionSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Observable.FromEventPattern(MyButton, "Clicked")
                .Subscribe(x => MyButton.Text = "Clicked!");
        }
    }
}
