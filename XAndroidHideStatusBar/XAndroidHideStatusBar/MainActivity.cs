using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace XAndroidHideStatusBar
{
    [Activity(Label = "XAndroidHideStatusBar", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var systemUiFlags = SystemUiFlags.LayoutStable
                                | SystemUiFlags.LayoutHideNavigation
                                | SystemUiFlags.LayoutFullscreen
                                | SystemUiFlags.HideNavigation
                                | SystemUiFlags.Fullscreen
                                | SystemUiFlags.Immersive;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(int)systemUiFlags;

            SetContentView(Resource.Layout.Main);
        }
    }
}

