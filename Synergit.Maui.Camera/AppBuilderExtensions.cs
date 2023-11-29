namespace Synergit.Maui.Camera;

public static class AppBuilderExtensions
{
    public static MauiAppBuilder UseSynergitCamera(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(h =>
        {
            h.AddHandler(typeof(CameraView), typeof(CameraViewHandler));
        });
        return builder;
    }
}
