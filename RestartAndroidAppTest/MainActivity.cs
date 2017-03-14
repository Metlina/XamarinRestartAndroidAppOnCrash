using Android.App;
using Android.Widget;
using Android.OS;
using Java.Lang;

namespace RestartAndroidAppTest
{
    [Activity(Label = "RestartAndroidAppTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            MyApplication.activity = this;

            if (Intent.GetBooleanExtra("crash", false))
            {
                Toast.MakeText(this, "App restarted after crash", ToastLength.Short).Show();
            }

            var button = FindViewById<Button>(Resource.Id.button);

            button.Click += (sender, args) =>
            {
                int a = 1, b = 0, c;

                c = a/b;
            };
        }
    }
}

