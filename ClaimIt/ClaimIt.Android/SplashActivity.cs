﻿using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace ClaimIt.Droid
{
  [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    static readonly string TAG = "X:" + typeof(SplashActivity).Name;

    public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
    {
      base.OnCreate(savedInstanceState, persistentState);
      Log.Debug(TAG, "SplashActivity.OnCreate");
    }

    // Launches the startup task
    protected override void OnResume()
    {
      base.OnResume();
      Task startupWork = new Task(() => { SimulateStartup(); });
      startupWork.Start();
    }

    // Prevent the back button from canceling the startup process
    public override void OnBackPressed() { }

    // Simulates background work that happens behind the splash screen
    async void SimulateStartup()
    {
      Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
      await Task.Delay(0); // Simulate a bit of startup work.
      Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
      StartActivity(new Intent(Application.Context, typeof(MainActivity)));
    }
  }
}