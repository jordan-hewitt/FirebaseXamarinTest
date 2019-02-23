using System;
using Firebase.Firestore;

namespace FirebaseXamarinTest.Droid.Services
{
    /* ******************************************************************
     * Compiler is unable to find the projectId in google-services.json, 
     * so this harness properly initializes Firestore 
     * ******************************************************************/

    public static class FirestoreService
    {
        private static Firebase.FirebaseApp app;

        public static FirebaseFirestore Instance
        {
            get
            {
                return FirebaseFirestore.GetInstance(app);
            }
        }

        public static string AppName { get; } = "FirebaseXamarinTest";

        public static void Init(Android.Content.Context context)
        {
            var baseOptions = Firebase.FirebaseOptions.FromResource(context);
            var options = new Firebase.FirebaseOptions.Builder(baseOptions).SetProjectId("fir-xamarintest-626a4").Build();
            app = Firebase.FirebaseApp.InitializeApp(context, options, AppName);
        }
    }
}
