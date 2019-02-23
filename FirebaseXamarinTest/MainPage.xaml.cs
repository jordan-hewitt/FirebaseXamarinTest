using Xamarin.Forms;
using FirebaseXamarinTest.Views;
using FirebaseXamarinTest.Interfaces;

namespace FirebaseXamarinTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Register_Clicked(object sender, System.EventArgs e)
        {
            if (CheckValidatiions())
            {
                var error = await DependencyService.Get<IFirebaseAuthenticator>().RegsiterWithEmailPassword(txtEmail.Text, txtPassword.Text);

                if (!string.IsNullOrEmpty(error))
                    await DisplayAlert("Alert", error, "Ok");
                else
                    await Navigation.PushAsync(new PhoneNumberPage());
            }
            else
            {
                await DisplayAlert("Alert", "A username or password must be entered", "OK");
            }
        }

        async void Login_Cicked(object sender, System.EventArgs e)
        {
            if (CheckValidatiions())
            {
                var error = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailPassword(txtEmail.Text, txtPassword.Text);

                if (!string.IsNullOrEmpty(error))
                    await DisplayAlert("Alert", error, "OK");
                else
                    await Navigation.PushAsync(new AddProfilePage());
            }
            else
            {
                await DisplayAlert("Alert", "A username or password must be entered", "OK");
            }
        }

        async void DeleteAccount_Clicked(object sender, System.EventArgs e)
        {
            string error = "";

            if (CheckValidatiions())
                error = await DependencyService.Get<IFirebaseAuthenticator>().DeleteAccount(txtEmail.Text, txtPassword.Text);
            else
                error = await DependencyService.Get<IFirebaseAuthenticator>().DeleteAccount();

            if (!string.IsNullOrEmpty(error))
                await DisplayAlert("Alert", error, "OK");
            else
                await DisplayAlert("Success", "Your account has been deleted", "OK");
        }

        private bool CheckValidatiions()
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                return false;
            }

            return true;
        }
    }
}
