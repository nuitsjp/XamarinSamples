using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XFCustomRenderer
{
    public class App : Application
    {
        public App()
        {
            var rootPage = new ContentPage();

            var toRoundedBoxView = new Button
            {
                Text = "RoundedBoxView"
            };
            toRoundedBoxView.Clicked += async (s, e) =>
                await rootPage.Navigation.PushAsync(new RoundedBoxViewPage());

            var toEventBasedWebView = new Button
            {
                Text = "EventBasedWebView"
            };
            toEventBasedWebView.Clicked += async (s, e) =>
                await rootPage.Navigation.PushAsync(new EventBasedWebViewPage());

            var toMessageBasedWebView = new Button
            {
                Text = "MessageBasedWebView"
            };
            toMessageBasedWebView.Clicked += async (s, e) =>
                await rootPage.Navigation.PushAsync(new MessageBasedWebViewPage());

            rootPage.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    toRoundedBoxView,
                    toEventBasedWebView,
                    toMessageBasedWebView,
                },
            };

            MainPage = new NavigationPage(rootPage);
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
