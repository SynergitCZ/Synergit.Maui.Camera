namespace Synergit.Maui.Camera.Test.ViewModels;

public partial class CameraPreviewViewModel : BaseViewModel
{
    public override void Prepare(Guid id, Dictionary<string, object> otherParams)
    {
        var parameter = (CameraPreviewParam)otherParams["CameraPreviewParam"];

        this.Preview = parameter.Picture;
 
        this.OnPropertyChanged(nameof(this.Preview));
    }

    public byte[] Preview { get; private set; }
}
