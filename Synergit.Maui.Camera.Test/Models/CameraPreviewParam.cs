namespace Synergit.Maui.Camera.Test.Models;

public class CameraPreviewParam : BaseModel
{   
    public CameraPreviewParam(byte[] picture) 
    {
        this.Picture = picture;
    }

    public byte[] Picture { get; set; }
}
