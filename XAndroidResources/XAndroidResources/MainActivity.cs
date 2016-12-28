using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Xml;

namespace XAndroidResources
{
    [Activity(Label = "XAndroidResources", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            string message;
            using (var stream = new StreamReader(Assets.Open("MyXml.xml")))
            {
                XmlDocument document = new XmlDocument();
                document.Load(stream);
                message = document.ChildNodes[1].InnerText;
            }

            SetContentView (Resource.Layout.Main);

            var messageTextView = FindViewById<TextView>(Resource.Id.MessageTextView);
            messageTextView.Text = message;
        }
    }
}

