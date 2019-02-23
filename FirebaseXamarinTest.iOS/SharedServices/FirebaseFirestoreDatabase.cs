using Xamarin.Forms;
using Xamarin.Essentials;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.CloudFirestore;
using FirebaseXamarinTest.Models;
using FirebaseXamarinTest.Interfaces;

[assembly: Dependency(typeof(FirebaseXamarinTest.iOS.FirebaseFirestoreDatabase))]
namespace FirebaseXamarinTest.iOS
{
    public class FirebaseFirestoreDatabase : IFirebaseFirestoreDatabase
    {
        public async Task<string> SetProfile(Profile profile)
        {
            await Task.Delay(100);

            try
            {
                Dictionary<object, object> profileDict = new Dictionary<object, object>
                {
                    { "First", profile.FirstName },
                    { "Last", profile.LastName },
                    { "Born", profile.BirthYear }
                };
                
                await Firestore.SharedInstance.GetCollection("Profiles").GetDocument(await SecureStorage.GetAsync("userID")).SetDataAsync(profileDict);
                
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
