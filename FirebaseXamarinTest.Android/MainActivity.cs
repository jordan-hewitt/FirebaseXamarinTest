
using Android.App;
using Android.Content.PM;

using Android.OS;
using Firebase.Firestore;
using Plugin.CurrentActivity;
using FirebaseXamarinTest.Droid.Services;

namespace FirebaseXamarinTest.Droid
{
    [Activity(Label = "FirebaseXamarinTest", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Firebase.FirebaseApp.InitializeApp(Application.Context);
            FirestoreService.Init(Application.Context);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Firebase.FirebaseApp.InitializeApp(this);
            LoadApplication(new App());
        }
    }
}