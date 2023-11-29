namespace Synergit.Maui.Camera.Test.Services;

public static class Resolver
{
    private static IServiceProvider serviceProvider;
    public static IServiceProvider ServiceProvider => serviceProvider ?? throw new ObjectDisposedException("ServiceProvider", "Service provider has not been initialized");

    public static void RegisterServiceProvider(IServiceProvider sp)
    {
        serviceProvider = sp;
    }

    public static T Resolve<T>() where T : class
        => ServiceProvider.GetRequiredService<T>();
}
