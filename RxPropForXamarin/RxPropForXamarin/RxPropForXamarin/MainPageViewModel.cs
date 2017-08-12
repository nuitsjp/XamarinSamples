using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace RxPropForXamarin
{
    public class MainPageViewModel
    {
        private readonly ReactiveProperty<int> _counter = new ReactiveProperty<int>(0);
        public ReactiveProperty<string> Message { get; }

        public ReactiveCommand UpdateMessageCommand { get; }

        public MainPageViewModel()
        {
            Message = _counter.Select(x => $"Number of update executions:{x}").ToReactiveProperty();

            UpdateMessageCommand = new ReactiveCommand();
            UpdateMessageCommand.Subscribe(_ => { _counter.Value++; });
        }
    }
}
