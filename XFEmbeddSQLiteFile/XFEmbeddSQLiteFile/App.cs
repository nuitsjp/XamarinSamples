using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using PCLStorage;
using SQLite;
using Xamarin.Forms;

namespace XFEmbeddSQLiteFile
{
    public class App : Application
    {
        public App()
        {
            var label = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Welcome to Xamarin Forms!"
            };
            var button = new Button { Text = "Load Database" };
            // The root page of your application
            var content = new ContentPage
            {
                Title = "XFEmbeddSQLiteFile",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        label,
                        button
                    }
                }
            };

            button.Clicked += async (sender, args) =>
            {
                const string databaseFileName = "sqlite.db3";
                // ルートフォルダを取得する
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                // DBファイルの存在チェックを行う
                var result = await rootFolder.CheckExistsAsync(databaseFileName);
                if (result == ExistenceCheckResult.NotFound)
                {
                    // リソースからファイルを読み込む
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    using (var stream = assembly.GetManifestResourceStream("XFEmbeddSQLiteFile.sqlite.db3"))
                    {
                        // 存在しなかった場合、新たにDBファイルを作成しテーブルも併せて新規作成する
                        var newFile = await rootFolder.CreateFileAsync(databaseFileName, CreationCollisionOption.ReplaceExisting);
                        using (var outputStream = await newFile.OpenAsync(FileAccess.ReadAndWrite))
                        {
                            stream.CopyTo(outputStream);
                            outputStream.Flush();
                        }
                    }
                }

                // 存在した場合、そのままコネクションを作成する
                var file = await rootFolder.CreateFileAsync(databaseFileName, CreationCollisionOption.OpenIfExists);
                using (var connection = new SQLiteConnection(file.Path))
                {
                    var builder = new StringBuilder();
                    foreach (var customer in connection.Table<Customer>())
                    {
                        builder.Append($"Id:{customer.Id} Name:{customer.Name}, ");
                    }
                    label.Text = builder.ToString();
                }
            };

            MainPage = new NavigationPage(content);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
