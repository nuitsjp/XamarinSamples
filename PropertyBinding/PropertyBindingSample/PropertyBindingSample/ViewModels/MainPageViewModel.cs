using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using PropertyBindingSample.Models;
using Reactive.Bindings.Extensions;

namespace PropertyBindingSample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IApplicationModel _applicationModel;

        public MainPageViewModel(IApplicationModel applicationModel)
        {
            _applicationModel = applicationModel;
            //ReactiveBinder.Bind(() => _applicationModel.Title, () => Title);
            //ReactiveBinder.Bind(_applicationModel.ObserveProperty(x => x.Title), () => Title);
            _applicationModel
                .ObserveProperty(x => x.Title)
                .Select(x => x + " append.")
                .Bind(() => Title);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                _applicationModel.Title = parameters["title"] as string;
        }
    }
}
