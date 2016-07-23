namespace XFStopwatch.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new Views.App());
        }
    }
}
