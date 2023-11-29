namespace Synergit.Maui.Camera.Test.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    private static BaseViewModel currentViewModel;
    private bool initialized;
    private bool isBusy;

    protected BaseViewModel()
    {
        this.NavigationService = Resolver.Resolve<INavigationService>();
        this.Caller = currentViewModel;
    }

    public virtual void Prepare(Guid id, Dictionary<string, object> otherParams)
    {
    }

    public void Initialize()
    {
        currentViewModel = this;

        async void loadAsync()
        {
            if (!this.initialized || this.RefreshPending)
            {
                var initializing = !this.initialized;
                this.initialized = true;
                this.RefreshPending = false;
                try
                {
                    await this.InternalLoadData(initializing);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayPromptAsync("Error", ex.InnerException?.Message ?? ex.Message);
                }
            }
        }
        loadAsync();
    }

    public async Task Refresh()
    {
        await this.InternalLoadData(false);
    }

    internal static void ResetCaller(bool resetCurrentToo)
    {
        if (currentViewModel != null)
        {
            currentViewModel.Caller = null;
        }

        if (resetCurrentToo)
        {
            currentViewModel = null;
        }
    }

    public event EventHandler LoadDataCompleted;

    protected virtual Task LoadData(bool initializing)
    {
        return Task.CompletedTask;
    }

    private async Task InternalLoadData(bool initializing)
    {
        this.IsBusy = true;

        try
        {
            await this.LoadData(initializing);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.InnerException?.Message ?? ex.Message, "Ok");
            if (initializing && this.Caller != null)
                await this.NavigationService.CloseActivePage();
        }
        finally
        {
            this.IsBusy = false;
            LoadDataCompleted?.Invoke(this, EventArgs.Empty);
        }
    }

    #region INotifyPropertyChanged implementation

    public event PropertyChangedEventHandler PropertyChanged;

    internal void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion


    public bool IsBusy
    {
        get => this.isBusy || !this.initialized;
        set { this.isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
    }

    internal INavigationService NavigationService { get; }
    public BaseViewModel Self => this;
    internal bool RefreshPending { get; set; }
    public static BaseViewModel CurrentViewModel => currentViewModel;
    internal BaseViewModel Caller { get; private set; }
}
