using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Firebase.Auth;
using FirebaseXamarinTest.Interfaces;
using Xamarin.Essentials;
using Foundation;

[assembly: Dependency(typeof(FirebaseXamarinTest.iOS.FirebaseAuthenticator))]
namespace FirebaseXamarinTest.iOS
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        static string _phoneNumberCode;

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                var token =  await user.User.GetIdTokenAsync();

                await SecureStorage.SetAsync("userID", user.User.Uid);

                return await SetToken(token);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> RegsiterWithEmailPassword(string email, string password)
        {
            var currentuser = Auth.DefaultInstance.CurrentUser;

            try
            {
                var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
                var token = await user.User.GetIdTokenAsync();

                await SecureStorage.SetAsync("userID", user.User.Uid);

                return await SetToken(token);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> RegisterWithPhone(string phoneNumber)
        {
            var restructuredPhoneNumber = RestructuredPhoneNumber(phoneNumber);

            try
            {
                var works = await PhoneAuthProvider.DefaultInstance.VerifyPhoneNumberAsync(restructuredPhoneNumber, null);
                _phoneNumberCode = works;

                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> VerifyAccount(string verificationCode)
        {
            try
            {
                var phoneAuthCredential = PhoneAuthProvider.DefaultInstance.GetCredential(_phoneNumberCode, verificationCode);
                var authDataResult = await Auth.DefaultInstance.CurrentUser.LinkAndRetrieveDataAsync(phoneAuthCredential);

                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> DeleteAccount()
        {
            if (Auth.DefaultInstance.CurrentUser != null)
            {
                try
                {
                    await Auth.DefaultInstance.CurrentUser.DeleteAsync();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            return "There is not a current user logged in.";
        }

        public async Task<string> DeleteAccount(string email, string password)
        {
            if (Auth.DefaultInstance.CurrentUser != null)
            {
                try
                {
                    NSError error;
                    Auth.DefaultInstance.SignOut(out error);
                    var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                    await Auth.DefaultInstance.CurrentUser.DeleteAsync();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            return "There is not a current user logged in.";
        }

        private string RestructuredPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10)
            {
                phoneNumber = phoneNumber.Insert(3, "-");
                phoneNumber = phoneNumber.Insert(7, "-");
            }

            phoneNumber = phoneNumber.Insert(0, "+1-");

            return phoneNumber;
        }

        async Task<string> SetToken(string token)
        {
            try
            {
                await SecureStorage.SetAsync("user_token", token);

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
