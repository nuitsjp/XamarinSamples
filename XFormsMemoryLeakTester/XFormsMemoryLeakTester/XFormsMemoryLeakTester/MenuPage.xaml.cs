using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFormsMemoryLeakTester
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        async void OnClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}
