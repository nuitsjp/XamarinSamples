using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PCLStorage;

namespace PCLStorageSample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        public DelegateCommand LoadCommand => new DelegateCommand(Load);

        private async void Load()
        {
            var file = await GetTextFile();

            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            using (var reader = new StreamReader(stream))
            {
                Title = reader.ReadToEnd();
            }
        }

        private async void Save()
        {
            var file = await GetTextFile();
            
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(Title);
                writer.Flush();
            }
        }



        private static async Task<IFile> GetTextFile()
        {
            var folder = FileSystem.Current.LocalStorage;
            return await folder.CreateFileAsync("text.txt", CreationCollisionOption.OpenIfExists);
        }


        public MainPageViewModel()
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
    }
}
