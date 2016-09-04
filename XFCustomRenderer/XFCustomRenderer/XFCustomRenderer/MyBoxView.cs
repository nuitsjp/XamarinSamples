using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFCustomRenderer
{
    public class MyBoxView : View
    {
        // BindablePropertyを追加
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(MyBoxView), default(Color),
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((MyBoxView) bindable).Color = (Color) newValue);

        public Color Color
        {
            get { return (Color) GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }

        }

        // イベントを追加
        public event EventHandler Clicked;

        // Rendererからのシグナルを受け取る
        internal void SendClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

    }
}
