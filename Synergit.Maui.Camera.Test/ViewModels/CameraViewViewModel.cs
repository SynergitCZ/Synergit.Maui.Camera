namespace Synergit.Maui.Camera.Test.ViewModels;

public partial class CameraViewViewModel : BaseViewModel
{
    private int counter;

    public override void Prepare(Guid id, Dictionary<string, object> otherParams)
    {
        var parameter = (CameraViewParam)otherParams["CameraViewParam"];

        this.TargetForm = parameter.TargetForm;
        this.Counter = parameter.Counter;
    }

    public ICamera TargetForm { get; private set; }
    public int Counter { get { return counter; } set { counter = value; this.OnPropertyChanged(nameof(this.Counter)); } }
}
