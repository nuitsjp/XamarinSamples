using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XFormsBindingSample
{
    public class ElementNameSubstitutePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                if(message != value)
                {
                    bool previousCanExecute = CanExecute(message);  // 変更前の実行状態
                    bool nextCanExecute = CanExecute(value);        // 変更後の実行状態
                    message = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Message"));

                    if (previousCanExecute != nextCanExecute)
                        // 実行可能状態が変更されていた場合、コマンドの実行可能状態変更通知を投げてあげる
                        ExecuteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand ExecuteCommand { get; }

        public ElementNameSubstitutePageViewModel()
        {
            ExecuteCommand =
                new RelayCommand(
                    // 実行内容
                    () => { Message = "ボタンが押されましたよ！"; },
                    // 実行可否判定
                    () => { return CanExecute(Message); }
                );
        }

        private bool CanExecute(string value)
        {
            return !string.IsNullOrEmpty(value) && 8 <= value.Length;
        }
    }
}
