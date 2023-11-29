namespace Synergit.Maui.Camera.Test;

internal static partial class UnhandledExceptionHandler
{
    public static void SetHandlers()
    {
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
    }

    private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
    {
        var x = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
        LogUnhandledException(x);
    }

    private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
    {
        var x = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
        LogUnhandledException(x);
    }

    internal static void LogUnhandledException(Exception exception)
    {
        try
        {
            const string errorFileName = "Fatal.log";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var errorFilePath = Path.Combine(libraryPath, errorFileName);
            var errorMessage = string.Format("Time: {0}\r\nError: Unhandled Exception\r\n{1}", DateTime.Now, exception.ToString());
            File.WriteAllText(errorFilePath, errorMessage);

#if ANDROID
            // Log to Android Device Logging.
            Android.Util.Log.Error("Crash Report", errorMessage);
#endif
        }
        catch
        {
            // just suppress any error logging exceptions
        }
    }

    [Conditional("DEBUG")]
    internal static async void DisplayCrashReport()
    {
        // If there is an unhandled exception, the exception information is diplayed 
        // on screen the next time the app is started (only in debug configuration)

        const string errorFilename = "Fatal.log";
        var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var errorFilePath = Path.Combine(libraryPath, errorFilename);

        if (!File.Exists(errorFilePath))
        {
            return;
        }

        var errorText = File.ReadAllText(errorFilePath);

        var doClear = await Application.Current.MainPage.DisplayAlert("Crash Report", errorText, "Clear", "Close");
        if (doClear)
        {
            File.Delete(errorFilePath);
        }
    }

}
