using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CoolBreeze
{
    public partial class App : Application
    {
        public static MainViewModel ViewModel;
        public static string RegistrationCode = "XBWWAF935AB12C70D16FBCD0E0AAF1D3";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
