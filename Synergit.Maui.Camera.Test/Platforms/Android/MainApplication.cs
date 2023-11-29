using Android.App;
using Android.Runtime;
using AndroidX.AppCompat.App;

namespace Synergit.Maui.Camera.Test;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
    {
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;
        UnhandledExceptionHandler.SetHandlers();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
