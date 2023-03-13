using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Diagnostics;

namespace CAPP;

[Activity(Theme = "@style/SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);
        Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
        Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentNavigation);
        Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
        Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        base.OnCreate(savedInstanceState);
    }
}
