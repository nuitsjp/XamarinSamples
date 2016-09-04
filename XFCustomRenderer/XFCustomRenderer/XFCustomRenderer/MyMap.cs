using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFCustomRenderer
{
    public class MyMap : View
    {
        public void MoveToResion(double latitude, double longitude)
        {
            // Rendererにメッセージを送る
            MessagingCenter.Send(this, "MyMapMoveToRegion", new Tuple<double, double>(latitude, longitude));
        }

    }
}
