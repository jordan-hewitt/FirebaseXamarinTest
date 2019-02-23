using Xamarin.Forms;
using Firebase.Firestore;
using FirebaseXamarinTest.Models;
using FirebaseXamarinTest.Interfaces;
using FirebaseXamarinTest.Droid.Services;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Threading.Tasks;

[assembly: Dependency(typeof(FirebaseXamarinTest.Droid.SharedServices.FirebaseFirestoreDatabase))]
namespace FirebaseXamarinTest.Droid.SharedServices
{
    public class FirebaseFirestoreDatabase : IFirebaseFirestoreDatabase
    {
        public async Task<string> SetProfile(Profile profile)
        {
            try
            {
                DocumentReference docRef = FirestoreService.Instance.Collection("Profiles").Document(await SecureStorage.GetAsync("userID"));
                //JavaProfile javaProfile = new JavaProfile(profile.FirstName, profile.LastName, profile.BirthYear);
                Dictionary<string, Java.Lang.Object> profileDict = new Dictionary<string, Java.Lang.Object>
                {
                    { "First", profile.FirstName },
                    { "Last", profile.LastName },
                    { "Born", profile.BirthYear }
                };

                docRef.Set(profileDict);
            }
            catch (FirebaseFirestoreException e)
            {
                return e.Message;
            }

            return "";
        }
    }
}
