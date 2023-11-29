namespace Synergit.Maui.Camera.Test.Models;

public partial class Attachment : BaseModel
{
    public Attachment(ObservableCollection<Attachment> collection, Guid id, string name, byte[] filedata, bool alreadySaved=false)
    {
        this.ID = id;
        this.Name = name;
        this.FileData = filedata;
        this.AlreadySaved = alreadySaved;
        this.Collection = collection;
    }

    public Guid ID { get; private set; }
    public string Name { get; private set; }
    public byte[] FileData { get; private set; }
    public bool AlreadySaved { get; set; }
    public double Opacity => AlreadySaved ? 0.5 : 1.0;


    public ObservableCollection<Attachment> Collection { get; }

    public async Task PreviewAttachment()
    {
        var navigationService  = Resolver.Resolve<INavigationService>();
        var param = new CameraPreviewParam(FileData);
        await navigationService.ShowPageEx<CameraPreviewPage>(Guid.NewGuid(), new() { { "CameraPreviewParam", param } });
    }

    public void DeleteAttachment()
    {
        Collection.Remove(this);
    }

    public Command PreviewAttachmentCommand => new Command(async ()=> await this.PreviewAttachment());
    public Command DeleteAttachmentCommand => new Command(() => this.DeleteAttachment());

}
