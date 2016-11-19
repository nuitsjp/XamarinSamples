using System;
using Xamarin.Forms;

namespace OnBackButtonPressed.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.SecondPage());
        }
    }
}
