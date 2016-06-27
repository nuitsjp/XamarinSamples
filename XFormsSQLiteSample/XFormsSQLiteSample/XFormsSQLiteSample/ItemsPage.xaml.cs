using PCLStorage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XFormsSQLiteSample
{
    public partial class ItemsPage : ContentPage
    {
        public string Value { get; set; }
        public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

        public ItemsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            using (var connection = await CreateConnection())
            {
                // Id順にソートして取得する
                foreach (var item in (from x in connection.Table<Item>() orderby x.Id select x))
                {
                    Items.Add(item);
                }
            }
        }

        public async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                using (var connection = await CreateConnection())
                {
                    var item = new Item { Value = Value };
                    connection.Insert(item);
                    Items.Add(item);
                }
            }
        }

        private async Task<SQLiteConnection> CreateConnection()
        {
            const string DatabaseFileName = "item.db3";
            // ルートフォルダを取得する
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            // DBファイルの存在チェックを行う
            var result = await rootFolder.CheckExistsAsync(DatabaseFileName);
            if (result == ExistenceCheckResult.NotFound)
            {
                // 存在しなかった場合、新たにDBファイルを作成しテーブルも併せて新規作成する
                IFile file = await rootFolder.CreateFileAsync(DatabaseFileName, CreationCollisionOption.ReplaceExisting);
                var connection = new SQLiteConnection(file.Path);
                connection.CreateTable<Item>();
                return connection;
            }
            else
            {
                // 存在した場合、そのままコネクションを作成する
                IFile file = await rootFolder.CreateFileAsync(DatabaseFileName, CreationCollisionOption.OpenIfExists);
                return new SQLiteConnection(file.Path);
            }
        }

    }
}
