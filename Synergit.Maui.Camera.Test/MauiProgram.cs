using Microsoft.Extensions.Logging;

namespace Synergit.Maui.Camera.Test
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSynergitCamera()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                .AddSingleton<INavigationService, NavigationService>()
                .AddTransientAllDescendantsOf<BaseViewModel>()
                .AddTransientAllDescendantsOf<BasePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            app.Services.UseResolver();

            return app;
        }
    }
}
