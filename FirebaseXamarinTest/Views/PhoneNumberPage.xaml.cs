using Xamarin.Forms;
using FirebaseXamarinTest.Interfaces;

namespace FirebaseXamarinTest.Views
{
    public partial class PhoneNumberPage : ContentPage
    {
        public PhoneNumberPage()
        {
            InitializeComponent();
        }

        async void ReceivePushNotification_Clicked(object sender, System.EventArgs e)
        {
            if (txtPhoneNumber.Text.Length == 10)
            {
                var error = await DependencyService.Get<IFirebaseAuthenticator>().RegisterWithPhone(txtPhoneNumber.Text);

                if (!string.IsNullOrEmpty(error))
                    await DisplayAlert("Alert", error, "OK");
                else
                    await Navigation.PushAsync(new VerificationCodePage());
            }
        }
    }
}
