using Xamarin.Forms;

namespace OnBackButtonPressed.Views
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
