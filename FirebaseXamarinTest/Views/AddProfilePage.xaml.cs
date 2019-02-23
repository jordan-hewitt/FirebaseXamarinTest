using System;
using FirebaseXamarinTest.Models;
using FirebaseXamarinTest.Interfaces;
using Xamarin.Forms;

namespace FirebaseXamarinTest.Views
{
    public partial class AddProfilePage : ContentPage
    {
        public AddProfilePage()
        {
            InitializeComponent();
        }

        async void AddProfile_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtFirstName.Text) &&
                !string.IsNullOrWhiteSpace(txtLastName.Text) &&
                !string.IsNullOrWhiteSpace(txtBirthYear.Text))
            {
                var profile = new Profile
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    BirthYear = Convert.ToInt32(txtBirthYear.Text)
                };

                var error = await DependencyService.Get<IFirebaseFirestoreDatabase>().SetProfile(profile);

                if (!string.IsNullOrEmpty(error))
                    await DisplayAlert("Alert", error, "OK");
                else
                    await DisplayAlert("Alert", "Successfully added the user's profile", "OK");
            }
        }
    }
}
