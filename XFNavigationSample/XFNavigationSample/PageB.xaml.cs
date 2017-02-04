using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFNavigationSample
{
    public partial class PageB : ContentPage
    {
        public Application CurrentApplication => App.CurrentApplication;
        public PageB()
        {
            InitializeComponent();
        }

        private void NavigateToPageBByModel(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageB());
        }

        private void NavigateToPageB(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageB());
        }

        private void NavigateToPageAByModal(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageA());
        }

        private void GoBackButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
