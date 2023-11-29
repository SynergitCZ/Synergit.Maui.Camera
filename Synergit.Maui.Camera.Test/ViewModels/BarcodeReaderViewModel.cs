using Synergit.Maui.Camera;
using Synergit.Maui.Camera.ZXingHelper;
using ZXing;

namespace Synergit.Maui.Camera.Test.ViewModels;

public partial class BarcodeReaderViewModel : BaseViewModel
{
    private bool isTorchOn;
    private CameraInfo camera;
    private ObservableCollection<CameraInfo> cameras = [];
    private Result[] barCodeResults;

    public BarcodeReaderViewModel()
    {
        BarCodeOptions = new ZXingHelper.BarcodeDecodeOptions
        {
            AutoRotate = true,
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
            ReadMultipleCodes = false,
            TryHarder = true,
            TryInverted = true
        };
        OnPropertyChanged(nameof(BarCodeOptions));
    }

    public void CameraLoaded(CameraView cameraView)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            Camera = Cameras[0];
        }
    }

    public ICamera TargetForm { get; private set; }

    public CameraInfo Camera
    {
        get => camera;
        set
        {
            camera = value;
            OnPropertyChanged(nameof(Camera));
            AutoStartPreview = false;
            OnPropertyChanged(nameof(AutoStartPreview));
            AutoStartPreview = true;
            OnPropertyChanged(nameof(AutoStartPreview));
        }
    }
    public ObservableCollection<CameraInfo> Cameras
    {
        get => cameras;
        set
        {
            cameras = value;
            OnPropertyChanged(nameof(Cameras));
        }
    }
    public bool AutoStartPreview { get; set; } = false;
    public bool IsTorchOn 
    { 
        get { return isTorchOn; } 
        set 
        { 
            isTorchOn = value; 
            this.OnPropertyChanged(nameof(this.IsTorchOn)); 
            this.OnPropertyChanged(nameof(this.TorchIcon));
        }
    }
    public string TorchIcon => this.IsTorchOn ? "torch_on.png" : "torch_off.png";
    public BarcodeDecodeOptions BarCodeOptions { get; set; }
    public string BarcodeText { get; set; } = "No barcode detected";
    public Result[] BarCodeResults
    {
        get => barCodeResults;
        set
        {
            barCodeResults = value;
            if (barCodeResults != null && barCodeResults.Length > 0)
                BarcodeText = barCodeResults[0].Text;
            else
                BarcodeText = "No barcode detected";
            OnPropertyChanged(nameof(BarcodeText));
        }
    }

}
