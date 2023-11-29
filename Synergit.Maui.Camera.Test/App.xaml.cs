namespace Synergit.Maui.Camera.Test;

public partial class App : Application
{
    public App(IServiceProvider services)
    {
        Application.Current.UserAppTheme = AppTheme.Light;

        InitializeComponent();

        var page = (Page)services.GetService<MainPage>();

        var navPage = new NavigationPage(page);
        navPage.Loaded += (o, e) => UnhandledExceptionHandler.DisplayCrashReport();

        this.MainPage = navPage;
    }

    public new static App Current => (App)Application.Current;
}
