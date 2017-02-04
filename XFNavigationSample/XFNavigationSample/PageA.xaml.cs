using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFNavigationSample
{
    public partial class PageA : ContentPage
    {
        public Application CurrentApplication => App.CurrentApplication;
        public PageA()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageB());
        }
    }
}
