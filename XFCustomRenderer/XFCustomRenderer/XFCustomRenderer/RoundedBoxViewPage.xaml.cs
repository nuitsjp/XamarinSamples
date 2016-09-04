using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFCustomRenderer
{
    public partial class RoundedBoxViewPage : ContentPage
    {
        public RoundedBoxViewPage()
        {
            InitializeComponent();
            var random = new Random();
            roundedBox.Clicked += (s, e) => {
                roundedBox.Color = Color.FromRgb(
                    random.Next(255),
                    random.Next(255),
                    random.Next(255));
            };
        }
    }
}
