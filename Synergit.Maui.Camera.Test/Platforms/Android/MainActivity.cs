using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Synergit.Maui.Camera;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var argb = Utils.FindResource<Color>("LightSeventhColor").ToInt();
        var color = new Android.Graphics.Color(argb);
        this.Window.SetNavigationBarColor(color);
        this.Window.SetStatusBarColor(color);
    }
}
