using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace FirebaseXamarinTest.Droid
{

    /* Code required from James Montegagno's Plugin.CurrentActivity to initialize
     * the plug-in 
     */

    #if DEBUG
    [Application(Debuggable = true)]
    #else
    [Application(Debuggable = false)]
    #endif
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }
    }
}
