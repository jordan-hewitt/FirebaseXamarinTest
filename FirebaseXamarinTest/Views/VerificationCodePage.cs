using System;

using Xamarin.Forms;

namespace FirebaseXamarinTest.Views
{
    public class VerificationCodePage : ContentPage
    {
        public VerificationCodePage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

