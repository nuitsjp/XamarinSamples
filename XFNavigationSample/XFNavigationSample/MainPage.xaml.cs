using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFNavigationSample
{
    public partial class MainPage : ContentPage
    {
        public Application CurrentApplication => App.CurrentApplication;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigateToMyNavigationPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MyNavigationPage());
        }

        private void NavigateToPageBByModel(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageB());
        }

        private void NavigateToMainPageByModel(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        private void NavigateToPageB(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageB());
        }
    }
}
