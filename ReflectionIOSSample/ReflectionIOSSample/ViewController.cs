using System;
using System.Reactive.Linq;
using System.Reflection;
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
            //Observable.FromEventPattern(MyButton, "TouchUpInside")
            //    .Subscribe(x => MyButton.SetTitle("Clicked!", UIControlState.Normal));


            var eventInfo = MyButton.GetType().GetRuntimeEvent("TouchUpInside");
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: Can't register the 'TouchUpInside' event.");

            // OnEventメソッドでイベントを購読するため、MethodInfoからデリゲートを作成しイベントへ追加する
            MethodInfo methodInfo = typeof(ViewController).GetTypeInfo().GetDeclaredMethod("OnEvent");
            var eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(MyButton, eventHandler);
        }

        private void OnEvent(object sender, EventArgs e)
        {
            MyButton.SetTitle("Clicked!", UIControlState.Normal);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}