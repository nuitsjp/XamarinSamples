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

        /// <summary>
        /// アプリケーション起動時の処理
        /// </summary>
        protected override async void OnAppearing()
        {
            // DBへのコネクションを取得してくる
            using (var connection = await CreateConnection())
            {
                // テーブルから登録済みの値を取得し、ObservableCollectionに突っ込んで画面にリスト表示する
                foreach (var item in (from x in connection.Table<Item>() orderby x.Id select x))
                {
                    Items.Add(item);
                }
            }
        }

        /// <summary>
        /// 追加ボタンを押下された場合のイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void OnButtonClicked(object sender, EventArgs e)
        {
            // Entryに値が設定されていた場合のみ処理する
            if (!string.IsNullOrEmpty(Value))
            {
                using (var connection = await CreateConnection())
                {
                    // Entryに設定されていた値をDBとListに追加する
                    var item = new Item { Value = Value };
                    connection.Insert(item);
                    Items.Add(item);
                }
            }
        }

        /// <summary>
        /// SQLiteデータベースへのコネクションを取得する。
        /// 取得したコネクションは取得した側で正しくクローズ処理すること。
        /// </summary>
        /// <returns></returns>
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
