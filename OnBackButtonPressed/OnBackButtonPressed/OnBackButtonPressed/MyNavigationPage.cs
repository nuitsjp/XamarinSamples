using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OnBackButtonPressed
{
    public class MyNavigationPage : NavigationPage
    {
        public MyNavigationPage(Page page) : base(page)
        {
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
