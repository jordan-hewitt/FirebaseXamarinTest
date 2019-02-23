using Xamarin.Forms;

using System.Threading.Tasks;
using Java.Util.Concurrent;
using Firebase;
using Firebase.Auth;
using FirebaseXamarinTest.Interfaces;
using Plugin.CurrentActivity;
using Android.App;
using System;
using Xamarin.Essentials;
using System.Diagnostics;

[assembly: Dependency(typeof(FirebaseXamarinTest.Droid.FirebaseAuthenticator))]
namespace FirebaseXamarinTest.Droid
{
    public class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        static string _verificationId;
        static string _registerPhoneAuthError;
        static PhoneAuthProvider.ForceResendingToken _token;

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);

                await SecureStorage.SetAsync("userID", user.User.Uid);

                return await SetToken(token.Token);
            }
            catch (FirebaseException e)
            {
                return e.Message;
            }
        }

        public async Task<string> RegsiterWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);

                await SecureStorage.SetAsync("userID", user.User.Uid);

                return await SetToken(token.Token);
            }
            catch (FirebaseException e)
            {
                return e.Message;
            }
        }

        public async Task<string> RegisterWithPhone(string phoneNumber)
        {
            await Task.Delay(100);
            var verificationStateChangedCallbacks = new VerificationStateChangedCallbacks();
            PhoneAuthProvider.Instance.VerifyPhoneNumber(RestructurePhoneNumber(phoneNumber), 60, TimeUnit.Seconds, CrossCurrentActivity.Current.Activity, verificationStateChangedCallbacks);

            return _registerPhoneAuthError;
        }

        internal class VerificationStateChangedCallbacks : PhoneAuthProvider.OnVerificationStateChangedCallbacks
        {
            public async override void OnVerificationCompleted(PhoneAuthCredential credential)
            {
                var a = await FirebaseAuth.Instance.SignInWithCredentialAsync(credential);
            }

            public override void OnVerificationFailed(FirebaseException exception)
            {
                if (exception is FirebaseAuthInvalidCredentialsException) 
                {
                    _registerPhoneAuthError = exception.Message;
                }
                else if (exception is FirebaseTooManyRequestsException) 
                {
                    _registerPhoneAuthError = exception.Message;
                }

                _registerPhoneAuthError = "";
            }

            public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
            {
                base.OnCodeSent(verificationId, forceResendingToken);

                _verificationId = verificationId;
                _token = forceResendingToken;
            }

            public override void OnCodeAutoRetrievalTimeOut(string verificationId)
            {
                base.OnCodeAutoRetrievalTimeOut(verificationId);

                _verificationId = verificationId;
            }
        }

        public async Task<string> VerifyAccount(string verificationCode)
        {

            try
            {
                var phoneAuthCredential = PhoneAuthProvider.GetCredential(_verificationId, verificationCode);
                var authResult = await FirebaseAuth.Instance.CurrentUser.LinkWithCredentialAsync(phoneAuthCredential);
                return "";
            }
            catch (FirebaseException e)
            {
                return e.Message;
            }
        }

        private string RestructurePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10)
            {
                phoneNumber = phoneNumber.Insert(3, "-");
                phoneNumber = phoneNumber.Insert(7, "-");
            }

            phoneNumber = phoneNumber.Insert(0, "+1-");

            return phoneNumber;
        }

        public async Task<string> DeleteAccount()
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                try
                {
                    await FirebaseAuth.Instance.CurrentUser.DeleteAsync();
                    return "";
                }
                catch (FirebaseException e)
                {
                    return e.Message;
                }
            }

            return "There is not a current user logged in.";
        }

        public async Task<string> DeleteAccount(string email, string password)
        {
            if (FirebaseAuth.Instance.CurrentUser != null)
            {
                try
                {
                    FirebaseAuth.Instance.SignOut();
                    var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                    await FirebaseAuth.Instance.CurrentUser.DeleteAsync();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            return "There is not a current user logged in.";
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
