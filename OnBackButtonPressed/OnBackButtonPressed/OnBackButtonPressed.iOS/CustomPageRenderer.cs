using System.Threading.Tasks;
using OnBackButtonPressed.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace OnBackButtonPressed.iOS
{
    public class CustomPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = Element as Page;
            var navigationPage = page.Parent as NavigationPage;
            var root = this.NavigationController.TopViewController;
            root.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem($"< Back", UIBarButtonItemStyle.Plain, async (sender, args) =>
            {
                var navPage = page.Parent as NavigationPage;
                var vm = page.BindingContext as IConfirmGoBack;

                if (vm != null)
                {
                    if (await vm.CanGoBackAsync())
                        navPage.PopAsync();
                }
                else
                    navPage.PopAsync();
            }), true);
        }
    }
}