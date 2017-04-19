using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CoolBreeze
{
    public partial class SubmissionPage : ContentPage
    {
        public SubmissionPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            this.BindingContext = App.ViewModel;
            base.OnAppearing();
        }
    }
}