using CoolBreeze.Common;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace CoolBreeze
{
    public class SubmissionInformation : Common.ObservableBase
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return this._isBusy; }
            set { this.SetProperty(ref this._isBusy, value); }
        }

        private bool _isSubmitted;
        public bool IsSubmitted
        {
            get { return this._isSubmitted; }
            set { this.SetProperty(ref this._isSubmitted, value); }
        }

        public string SubmissionIcon
        {
            get { return "key"; }
        }

        private string _submitLabel;
        public string SubmitLabel
        {
            get { return this._submitLabel; }
            set { this.SetProperty(ref this._submitLabel, value); }
        }

        private bool _canSubmit() => (Microsoft.Azure.Mobile.MobileCenter.InstallId.HasValue);

        private string _submissionLocation = "https://traininglabs.blob.core.windows.net/challenge-submissions?sv=2015-12-11&si=challenge-submissions-15AA6639B2C&sr=c&sig=lRYUs6%2FdEc3BfEMkKLlUtVOLeYuUhb%2F2juyVP1ms6UY%3D";
        private string _registrationLocation = $"http://traininglabservices.azurewebsites.net/api/weather/register?registrationCode={App.RegistrationCode}";

        public System.Windows.Input.ICommand SubmitChallengeCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    this.IsBusy = true;

                    await SubmitChallengeAsync(App.RegistrationCode);

                    this.IsSubmitted = true;
                    this.IsBusy = false;

                }, _canSubmit);
            }
        }

        public async Task<bool> SubmitChallengeAsync(string registrationCode)
        {
            bool successful = false;

            CloudBlobContainer container = new CloudBlobContainer(new Uri(_submissionLocation));

            var blob = container.GetBlockBlobReference($"{App.RegistrationCode}.submission");

            try
            {
                var finalSubmission = new
                {
                    InstallId = Microsoft.Azure.Mobile.MobileCenter.InstallId.ToString(),
                    RegistrationCode = App.RegistrationCode,
                };

                await blob.UploadTextAsync(Newtonsoft.Json.JsonConvert.SerializeObject(finalSubmission));

                successful = true;
            }
            catch (Exception ex)
            {
            }

            return successful;
        }

        public async void RegisterAsync(string registrationCode)
        {
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            await client.PostAsync(_registrationLocation, null);
        }
    }

}