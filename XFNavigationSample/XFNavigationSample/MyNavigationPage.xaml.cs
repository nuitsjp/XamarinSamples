using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFNavigationSample
{
    public partial class MyNavigationPage : NavigationPage
    {
        public Application CurrentApplication => App.CurrentApplication;
        public MyNavigationPage() : base(new PageA())
        {
            InitializeComponent();
        }
    }
}
