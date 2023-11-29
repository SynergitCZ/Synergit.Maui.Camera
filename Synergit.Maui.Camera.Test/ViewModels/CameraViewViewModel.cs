namespace Synergit.Maui.Camera.Test.ViewModels;

public partial class CameraViewViewModel : BaseViewModel
{
    private int counter;
    private bool isFlashOn;
    private bool isTorchOn;
    private bool hasPreview;
    private bool btnIsEnabled = true;
    private CameraInfo camera = null;
    private ObservableCollection<CameraInfo> cameras = new();
    private MicrophoneInfo microphone = null;
    private ObservableCollection<MicrophoneInfo> microphones = new();
    private float minZoom = 1f;
    private float maxZoom = 1f;
    private bool autostartPreview = false;

    public override void Prepare(Guid id, Dictionary<string, object> otherParams)
    {
        var parameter = (CameraViewParam)otherParams["CameraViewParam"];

        this.TargetForm = parameter.TargetForm;
        this.Counter = parameter.Counter;
        this.IsFlashOn = (Settings.CameraIsFlashOn == "1");
        this.HasPreview = (Settings.CameraHasPreview == "1");     

        this.CameraWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
        this.CameraHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
        this.ScreenRatio = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Width;
        this.OnPropertyChanged(nameof(CameraWidth));
        this.OnPropertyChanged(nameof(CameraHeight));
        this.OnPropertyChanged(nameof(ScreenRatio));
    }

    public void CameraLoaded(CameraView cameraView)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            if (cameraView.NumMicrophonesDetected > 0)
                Microphone = Microphones[0];

            Camera = Cameras[0];
        }
    }

    public void SaveSettings()
    {
        Settings.CameraIsFlashOn = (this.IsFlashOn) ? "1" : "0";
        Settings.CameraHasPreview = (this.HasPreview) ? "1" : "0";
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
            AutoStartPreview = true;
            OnPropertyChanged(nameof(this.FlashMode));
            OnPropertyChanged(nameof(this.IsZoomVisible));
            this.MinZoom = camera.MinZoomFactor;
            this.MaxZoom = (camera.MaxZoomFactor > 3.5f) ? 3.5f : camera.MaxZoomFactor;
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
    public MicrophoneInfo Microphone
    {
        get => microphone;
        set
        {
            microphone = value;
            OnPropertyChanged(nameof(Microphone));
        }
    }
    public ObservableCollection<MicrophoneInfo> Microphones
    {
        get => microphones;
        set
        {
            microphones = value;
            OnPropertyChanged(nameof(Microphones));
        }
    }
    public bool AutoStartPreview { get { return autostartPreview; } set { autostartPreview = value; OnPropertyChanged(nameof(AutoStartPreview)); } }
    public int Counter { get { return counter; } set { counter = value; this.OnPropertyChanged(nameof(this.Counter)); } }
    public bool IsFlashOn
    {
        get { return isFlashOn; }
        set
        {
            isFlashOn = value;
            this.OnPropertyChanged(nameof(this.IsFlashOn));
            this.OnPropertyChanged(nameof(this.FlashIcon));
            this.OnPropertyChanged(nameof(this.FlashMode)); 
        } 
    }
    public string FlashIcon => this.IsFlashOn ? "flash_on.png" : "flash_off.png";
    public FlashMode FlashMode => (isFlashOn && !isTorchOn) ? FlashMode.Enabled : FlashMode.Disabled;
    public bool IsTorchOn 
    { 
        get { return isTorchOn; } 
        set 
        { 
            isTorchOn = value; 
            this.OnPropertyChanged(nameof(this.IsTorchOn)); 
            this.OnPropertyChanged(nameof(this.TorchIcon));
            this.OnPropertyChanged(nameof(this.FlashMode));
        }
    }
    public string TorchIcon => this.IsTorchOn ? "torch_on.png" : "torch_off.png";
    public bool HasPreview { get { return hasPreview; } set { hasPreview = value; this.OnPropertyChanged(nameof(this.HasPreview)); this.OnPropertyChanged(nameof(this.PreviewIcon)); } }
    public string PreviewIcon => this.HasPreview ? "preview_on.png" : "preview_off.png";
    public bool BtnIsEnabled { get { return btnIsEnabled; } set { btnIsEnabled = value; this.OnPropertyChanged(nameof(this.BtnIsEnabled)); } }
    public bool IsZoomVisible => camera?.MaxZoomFactor > 1f;
    public float MinZoom { get { return minZoom; } set { minZoom = value; this.OnPropertyChanged(nameof(this.MinZoom)); } }
    public float MaxZoom { get { return maxZoom; } set { maxZoom = value; this.OnPropertyChanged(nameof(this.MaxZoom)); } }
    public byte[] Preview { get; set; }

    public double ScreenRatio { get; set; }
    public double CameraWidth { get; set; }
    public double CameraHeight { get; set; }
}
