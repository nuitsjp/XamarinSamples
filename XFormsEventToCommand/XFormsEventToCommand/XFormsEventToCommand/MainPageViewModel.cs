using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XFormsEventToCommand
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Message"));
            }
        }

        public ICommand AppearingCommand { get; }

        public MainPageViewModel()
        {
            AppearingCommand = new Command(() =>
            {
                Message = "Executed AppearingCommand!";
            });
        }
    }

}
