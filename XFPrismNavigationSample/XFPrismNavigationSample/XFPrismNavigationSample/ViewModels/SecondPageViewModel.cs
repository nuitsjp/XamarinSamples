using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Commands;

namespace XFPrismNavigationSample.ViewModels
{
    public class SecondPageViewModel
    {
        public ICommand GoBackCommand { get; set; }
        private readonly INavigationService _navigationService;
        public SecondPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand = new DelegateCommand(() => _navigationService.GoBackAsync());
        }
    }
}
