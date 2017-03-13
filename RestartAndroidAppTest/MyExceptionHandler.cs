using System;
using Android.App;
using Android.Content;
using Java.Lang;

namespace RestartAndroidAppTest
{
    public class MyExceptionHandler : Thread.IUncaughtExceptionHandler
    {
        private Activity activity;

        public MyExceptionHandler(Activity a)
        {
            activity = a;
        }

        public void UncaughtException(Thread t, Throwable e)
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

        public void Dispose()
        {
        }

        public IntPtr Handle { get; }
    }
}