using System;
using System.Collections.Generic;
using System.Text;
using CoreLocation;
using MapKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFCustomRenderer;
using XFCustomRenderer.iOS;

[assembly: ExportRenderer(typeof(MyMap), typeof(MyMapRenderer))]
namespace XFCustomRenderer.iOS
{
    public class MyMapRenderer : ViewRenderer<MyMap, MKMapView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MyMap> e)
        {
            if (Control == null)
            {
                var nativeControl = new MKMapView();
                SetNativeControl(nativeControl);
            }

            // Formsコントロールからのメッセージを受け取る
            MessagingCenter.Subscribe<MyMap, Tuple<double, double>>(this, "MyMapMoveToRegion",
                                                                     (sender, args) => MoveToRegion(args.Item1, args.Item2), Element);
            base.OnElementChanged(e);
        }

        private void MoveToRegion(double latitude, double longitude)
        {
            var mapRegion = new MKCoordinateRegion(
                    new CLLocationCoordinate2D(latitude, longitude),
                    new MKCoordinateSpan(1.0d, 1.0d));
            Control.SetRegion(mapRegion, true);
        }
    }
}
