namespace Synergit.Maui.Camera.Test;

public static class Settings
{
    #region Setting Constants
   
    private const string keyCameraIsFlashOn = "camera_isflashon";
    private const string keyCameraHasPreview = "camera_haspreview";

    #endregion

    public static string CameraIsFlashOn
    {
        get => Preferences.Default.Get(keyCameraIsFlashOn, "0");
        set => Preferences.Default.Set(keyCameraIsFlashOn, value);
    }

    public static string CameraHasPreview
    {
        get => Preferences.Default.Get(keyCameraHasPreview,"0");
        set => Preferences.Default.Set(keyCameraHasPreview, value);
    }

}