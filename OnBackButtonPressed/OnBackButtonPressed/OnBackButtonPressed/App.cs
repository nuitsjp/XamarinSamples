using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnBackButtonPressed.Views;
using Xamarin.Forms;

namespace OnBackButtonPressed
{
    public class App : Application
    {
        public App()
        {
            MainPage = new MyNavigationPage(new Views.MainPage());
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
