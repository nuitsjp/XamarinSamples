using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(XFCustomRenderer.RoundedBoxView), typeof(XFCustomRenderer.UWP.RoundedBoxViewRenderer))]
namespace XFCustomRenderer.UWP
{
    public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, Windows.UI.Xaml.Shapes.Rectangle>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var nativeControl = new Windows.UI.Xaml.Shapes.Rectangle();
                    nativeControl.DataContext = Element;
                    nativeControl.SetBinding(Shape.FillProperty,
                        new Windows.UI.Xaml.Data.Binding
                        {
                            Converter = new ColorConverter(),
                            Path = new PropertyPath(RoundedBoxView.ColorProperty.PropertyName),
                        });
                    nativeControl.SetBinding(Windows.UI.Xaml.Shapes.Rectangle.RadiusXProperty,
                        new Windows.UI.Xaml.Data.Binding
                        {
                            Path = new PropertyPath(RoundedBoxView.CornerRadiusProperty.PropertyName),
                        });
                    nativeControl.SetBinding(Windows.UI.Xaml.Shapes.Rectangle.RadiusYProperty,
                        new Windows.UI.Xaml.Data.Binding
                        {
                            Path = new PropertyPath(RoundedBoxView.CornerRadiusProperty.PropertyName),
                        });
                    nativeControl.Tapped += (sender, args) => Element.SendClick();
                    SetNativeControl(nativeControl);
                }
            }
        }
    }
}
