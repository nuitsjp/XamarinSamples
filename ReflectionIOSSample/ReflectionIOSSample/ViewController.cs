using System;
using System.Reactive.Linq;
using UIKit;

namespace ReflectionIOSSample
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            Observable.FromEventPattern(MyButton, "TouchUpInside")
                .Subscribe(x => MyButton.SetTitle("Clicked!", UIControlState.Normal));
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}