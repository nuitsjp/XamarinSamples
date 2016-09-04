using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFCustomRenderer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        readonly Random _random = new Random();

        void MyBoxViewClicked(object sender, System.EventArgs e)
        {
            DisplayAlert("ViewRendererTutorial", "MyBoxView Clicked.", "OK");
        }

    }
}
