namespace Synergit.Maui.Camera.Test.ViewModels;

public partial class MainPageViewModel : BaseViewModel
{
    public override void Prepare(Guid id, Dictionary<string, object> otherParams)
    {
        /* empty */
    }

    public ObservableCollection<Attachment> AttachmentList { get; } = [];
}
