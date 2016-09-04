using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFCustomRenderer;
using XFCustomRenderer.iOS;

[assembly: ExportRenderer(typeof(MyBoxView), typeof(MyBoxViewRenderer))]
namespace XFCustomRenderer.iOS
{

    public class MyBoxViewRenderer : ViewRenderer<MyBoxView, UIView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MyBoxView> e)
        {
            if (Control == null)
            {
                var nativeControl = new UIView();
                SetNativeControl(nativeControl);

                // NativeコントロールがタップされたらFormsコントロールにシグナルを送る
                nativeControl.AddGestureRecognizer(
                    new UITapGestureRecognizer(() => Element?.SendClicked()));
            }

            if (e.NewElement != null)
            {
                // Formsコントロールのプロパティ値を反映
                UpdateColor();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // プロパティ値の変更を反映
            if (e.PropertyName == MyBoxView.ColorProperty.PropertyName)
            {
                UpdateColor();
            }
        }

        private void UpdateColor()
        {
            if (Element == null)
                return;

            Control.BackgroundColor = Element.Color.ToUIColor();
        }


    }
}
