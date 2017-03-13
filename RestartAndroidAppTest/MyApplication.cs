using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Java.Lang;

namespace RestartAndroidAppTest
{
    [Application]
    public class MyApplication : Application
    {
        public static MyApplication instance;
        public static Activity activity;

        public static MyApplication GetInstance()
        {
            return instance;
        }

        public MyApplication(IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) { }

        public override void OnCreate()
        {
            base.OnCreate();
            instance = this;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironmentOnUnhandledExceptionRaiser;
        }

        private void AndroidEnvironmentOnUnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs raiseThrowableEventArgs)
        {
            var intent = new Intent(activity, typeof(MainActivity));
            intent.PutExtra("crash", true);
            intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.ClearTask | ActivityFlags.NewTask);

            var pendingIntent = PendingIntent.GetActivity(MyApplication.instance, 0, intent, PendingIntentFlags.OneShot);

            var mgr = (AlarmManager)MyApplication.instance.GetSystemService(Context.AlarmService);
            mgr.Set(AlarmType.Rtc, DateTime.Now.Millisecond + 100, pendingIntent);

            activity.Finish();

            JavaSystem.Exit(2);
        }

        protected override void Dispose(bool disposing)
        {
            AndroidEnvironment.UnhandledExceptionRaiser -= AndroidEnvironmentOnUnhandledExceptionRaiser;
            base.Dispose(disposing);
        }
    }
}