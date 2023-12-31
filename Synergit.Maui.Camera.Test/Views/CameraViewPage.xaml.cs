namespace Synergit.Maui.Camera.Test.Views;

public partial class CameraViewPage
{
    private bool isFlashOn = false;
    private bool hasPreview = false;

    public CameraViewPage(CameraViewViewModel viewModel) : base(viewModel)
    {
        this.InitializeComponent();

        CameraView.CamerasLoaded += CameraView_CamerasLoaded;
    }

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (CameraView.Cameras.Any() && CameraView.Cameras[0] is CameraInfo camera)
        {
            CameraView.Camera = camera;

            this.MinZoom = camera.MinZoomFactor;
            this.MaxZoom = (camera.MaxZoomFactor > 5f) ? 5f : camera.MaxZoomFactor;
            this.OnPropertyChanged(nameof(this.MinZoom));
            this.OnPropertyChanged(nameof(this.MaxZoom));

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (await CameraView.StartCameraAsync() != CameraResult.Success)
                {
                    await this.DisplayAlert("Error", "Problem with camera initialization.", "OK");
                }
            });
        }
    }

    protected override void OnSizeAllocated(double width, double height)
    {                                          
        base.OnSizeAllocated(width, height);

        if (height > width)
        {
            this.CameraBorder.WidthRequest = width;
            this.CameraBorder.HeightRequest = width * 1.33333333333334;
        }
        else
        {
            this.CameraBorder.HeightRequest = height;
            this.CameraBorder.WidthRequest = height * 1.33333333333334;
        }
    }

    private async void Capture(object sender, EventArgs e)
    {
        this.CaptureBtn.IsEnabled = false;
        this.OkBtn.IsEnabled = false;
        try
        {
            var stream = await CameraView.TakePhotoAsync();
            if (stream != null)
            {
                if (hasPreview)
                {
                    this.PreviewBorder.IsVisible = true;
                    Preview.RemoveBinding(Image.SourceProperty);
                    Preview.Source = ImageSource.FromStream(() => stream);
                    var bytes = stream.ReadAllBytes();
                    this.BindingContext.TargetForm.AddAttachement(bytes);
                    await Task.Delay(2500);
                    this.PreviewBorder.IsVisible = false;
                }
                else
                {
                    var bytes = stream.ReadAllBytes();
                    this.BindingContext.TargetForm.AddAttachement(bytes);
                }
                this.BindingContext.Counter++;
            }
        }
        finally
        {
            this.CaptureBtn.IsEnabled = true;
            this.OkBtn.IsEnabled = true;
        }
    }

    private async void OkBtn_Clicked(object sender, EventArgs e)
    {
        CameraView.TorchEnabled = false;
        await Task.Delay(100);
        await this.Navigation.PopAsync();
    }

    private void FlashMode_Tapped(object sender, EventArgs e)
    {
        this.isFlashOn = !this.isFlashOn;
        this.CameraView.FlashMode = this.isFlashOn ? FlashMode.Enabled : FlashMode.Disabled;
        this.FlashModeImg.Source = this.isFlashOn ? "flash_on.png" : "flash_off.png";
    }

    private void TorchMode_Tapped(object sender, EventArgs e)
    {
        this.CameraView.TorchEnabled = !this.CameraView.TorchEnabled;
        this.TorchModeImg.Source = this.CameraView.TorchEnabled ? "torch_on.png" : "torch_off.png";
    }

    private void PreviewMode_Tapped(object sender, EventArgs e)
    {
        this.hasPreview = !this.hasPreview;
        this.PreviewModeImg.Source = this.hasPreview ? "preview_on.png" : "preview_off.png";
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        var value = (float)e.NewValue;
        if (CameraView != null)
        {
            CameraView.ZoomFactor = value;
            CameraView.ForceAutoFocus();
        }
    }

    public float MinZoom { get; set; }
    public float MaxZoom { get; set; }
}