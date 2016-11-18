namespace OnBackButtonPressed
{
    public class SecondPageViewModel : IConfirmBack
    {
        public bool IsEnabled { get; set; }
        public bool CanGoBack()
        {
            return IsEnabled;
        }
    }
}