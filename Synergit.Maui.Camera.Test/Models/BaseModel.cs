namespace Synergit.Maui.Camera.Test.Models;

public class BaseModel : INotifyPropertyChanged
{
    #region INotifyPropertyChanged implementation

    public event PropertyChangedEventHandler PropertyChanged;

    internal void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}
