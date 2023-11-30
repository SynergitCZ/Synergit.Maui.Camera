namespace Synergit.Maui.Camera.Test.Views;

public partial class MainPage : ICamera
{

    public MainPage(MainPageViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    public void AddAttachement(byte[] fileData)
    {
        this.BindingContext.AttachmentList.Add(new Attachment(this.BindingContext.AttachmentList, Guid.NewGuid(), string.Format("{0:yyyyMMdd_hhmmss}.jpg", DateTime.Now), fileData));
    }

    private async void NormalCamera_Clicked(object sender, System.EventArgs e)
    {
        var navigationService = Resolver.Resolve<INavigationService>();
        var param = new CameraViewParam(this, this.BindingContext.AttachmentList.Count);
        await navigationService.ShowPageEx<CameraViewPage>(Guid.NewGuid(), new() { { "CameraViewParam", param } });
    }

    private async void MVVMCamera_Clicked(object sender, System.EventArgs e)
    {
        var navigationService = Resolver.Resolve<INavigationService>();
        var param = new CameraViewParam(this, this.BindingContext.AttachmentList.Count);
        await navigationService.ShowPageEx<CameraViewMVVMPage>(Guid.NewGuid(), new() { { "CameraViewParam", param } });
    }

    private async void BarcodeDetect_Clicked(object sender, System.EventArgs e)
    {
        var navigationService = Resolver.Resolve<INavigationService>();
        await navigationService.ShowPageEx<BarcodeReaderPage>();
    }
}
