using System.IO;

namespace Synergit.Maui.Camera.Test.Views;

public partial class CameraViewPage
{
    public CameraViewPage(CameraViewViewModel viewModel) : base(viewModel)
    {
        this.InitializeComponent();
    }

    private void CameraView_Loaded(object sender, EventArgs e)
    {
        this.BindingContext.CameraLoaded(CameraView);
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
        this.BindingContext.BtnIsEnabled = false;
        try
        {
            var stream = await CameraView.TakePhotoAsync();
            if (stream != null)
            {
                this.BindingContext.SaveSettings();

                if (this.BindingContext.HasPreview)
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
            this.BindingContext.BtnIsEnabled = true;
        }
    }

    private async void OkBtn_Clicked(object sender, EventArgs e)
    {
        this.BindingContext.SaveSettings();
        CameraView.TorchEnabled = false;
        await Task.Delay(100);
        await this.Navigation.PopAsync();
    }

    private void FlashMode_Tapped(object sender, EventArgs e)
    {
        this.BindingContext.IsFlashOn = !this.BindingContext.IsFlashOn;
    }

    private void TorchMode_Tapped(object sender, EventArgs e)
    {
        this.BindingContext.IsTorchOn = !this.BindingContext.IsTorchOn;
    }

    private void PreviewMode_Tapped(object sender, EventArgs e)
    {
        this.BindingContext.HasPreview = !this.BindingContext.HasPreview;
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
}