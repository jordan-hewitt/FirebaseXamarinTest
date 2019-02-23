using Xamarin.Forms;

using System;
using FirebaseXamarinTest.Interfaces;

namespace FirebaseXamarinTest.Views
{
    public partial class VerificationCodePage : ContentPage
    {
        public VerificationCodePage()
        {
            InitializeComponent();
        }

        async void VerifyAccount_Clicked(object sender, EventArgs e)
        {
            if (txtVerificationCode.Text.Length == 6)
            {
                var error = await DependencyService.Get<IFirebaseAuthenticator>().VerifyAccount(txtVerificationCode.Text);

                if (!string.IsNullOrEmpty(error))
                    await DisplayAlert("Alert", error, "OK");
                else
                {
                    await DisplayAlert("Success", "Account has successfully been verified", "OK");
                    await Navigation.PushAsync(new AddProfilePage());
                }
            }
        }
    }
}
