namespace Synergit.Maui.Camera.Test.Models;

public class CameraViewParam : BaseModel
{   
    public CameraViewParam(ICamera targetForm, int counter=0) 
    {
        this.TargetForm = targetForm;
        this.Counter = counter;
    }

    public ICamera TargetForm { get; }
    public int Counter { get;  }
}
