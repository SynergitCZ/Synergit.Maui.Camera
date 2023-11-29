//#if ANDROID
//    using OsSpecific = Synergit.Maui.Camera.Test.Platforms.Android;
//#elif IOS
//    using OsSpecific = Synergit.Maui.Camera.Test.Platforms.iOS.UIDevice;
//#elif MACCATALYST
//    using OsSpecific = Synergit.Maui.Camera.Test.Platforms.MacCatalyst.UIDevice;
//#endif

namespace Synergit.Maui.Camera.Test.Views;

public partial class BarcodeReaderPage
{
    public BarcodeReaderPage(BarcodeReaderViewModel viewModel) : base(viewModel)
    {
        this.InitializeComponent();
        //if (DeviceInfo.Platform == DevicePlatform.Android )
        //{
        //    OsSpecific.MainActivity.Instance.RequestedOrientation = OsSpecific.ScreenOrientation.Portrait;
        //}
        //else if (DeviceInfo.Platform == DevicePlatform.iOS )
        //{
        //    OsSpecific.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Portrait)), new NSString("orientation"));
        //}
        //else if (DeviceInfo.Platform == DevicePlatform.MacCatalyst)
        //{
        //    OsSpecific.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Portrait)), new NSString("orientation"));
        //}
    }

    private void CameraView_Loaded(object sender, EventArgs e)
    {
        this.BindingContext.CameraLoaded(CameraView);
    }

    private async void OkBtn_Clicked(object sender, EventArgs e)
    {
        CameraView.TorchEnabled = false;
        await Task.Delay(100);
        await this.Navigation.PopAsync();
    }

    private void TorchMode_Tapped(object sender, EventArgs e)
    {
        this.BindingContext.IsTorchOn = !this.BindingContext.IsTorchOn;
    }
}