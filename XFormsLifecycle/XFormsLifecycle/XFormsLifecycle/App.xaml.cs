using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XFormsLifecycle
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new XFormsLifecycle.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            (MainPage.BindingContext as IApplicationLifecycle)?.OnSleep();
        }

        protected override void OnResume()
        {
            (MainPage.BindingContext as IApplicationLifecycle)?.OnResume();
        }
    }
}
