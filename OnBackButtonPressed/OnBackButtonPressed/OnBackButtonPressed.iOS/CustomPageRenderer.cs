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
            var root = this.NavigationController.TopViewController;
            // NOTE: this doesn't look exactly right, you need to create an image to replicate the back arrow properly
            root.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem("< Back", UIBarButtonItemStyle.Plain, (sender, args) =>
            {
                var navPage = page.Parent as NavigationPage;
                var vm = page.BindingContext as IConfirmBack;

                if (vm != null)
                {
                    if (vm.CanGoBack())
                        navPage.PopAsync();
                }
                else
                    navPage.PopAsync();
            }), true);
        }
    }
}