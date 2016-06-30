using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            AppearingCommand = new RelayCommand(() =>
            {
                Message = "Executed AppearingCommand!";
            });
        }


        public class RelayCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private Action execute;

            public RelayCommand(Action execute)
            {
                this.execute = execute;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                execute();
            }
        }
    }

}
