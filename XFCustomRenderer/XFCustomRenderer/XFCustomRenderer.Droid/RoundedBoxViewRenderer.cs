using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(XFCustomRenderer.RoundedBoxView), typeof(XFCustomRenderer.Droid.RoundedBoxViewRenderer))]
namespace XFCustomRenderer.Droid
{
    public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, Android.Views.View>
    {
        Android.Graphics.Drawables.GradientDrawable controlBackground;

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
        {
            if (Control == null)
            {
                var nativeControl = new Android.Views.View(Context);
                controlBackground = new Android.Graphics.Drawables.GradientDrawable();
                nativeControl.Background = controlBackground;
                nativeControl.Click += OnClick;
                SetNativeControl(nativeControl);
            }

            if (e.NewElement != null)
            {
                // Formsコントロールのプロパティ値を反映
                UpdateRadius();
                UpdateColor();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // プロパティ値の変更を反映
            if (e.PropertyName == RoundedBoxView.CornerRadiusProperty.PropertyName)
            {
                UpdateRadius();
            }
            if (e.PropertyName == RoundedBoxView.ColorProperty.PropertyName)
            {
                UpdateColor();
            }
        }

        private void UpdateRadius()
        {
            var radiusDp = (float) (Element.CornerRadius*Resources.DisplayMetrics.Density);
            controlBackground.SetCornerRadius(radiusDp);
        }

        private void UpdateColor()
        {
            controlBackground.SetColor(Element.Color.ToAndroid());
        }

        private void OnClick(object sender, EventArgs e)
        {
            Element?.SendClick();
        }

        protected override void Dispose(bool disposing)
        {
            if (Control != null)
                Control.Click -= OnClick;

            base.Dispose(disposing);
        }
    }
}