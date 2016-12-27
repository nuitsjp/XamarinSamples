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

            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            SetContentView (Resource.Layout.Main);
        }
    }
}

