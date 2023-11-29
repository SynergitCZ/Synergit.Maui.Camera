namespace Synergit.Maui.Camera.Test.Views;

public abstract class BasePage<TViewModel> : BasePage where TViewModel : BaseViewModel
{
    protected BasePage(TViewModel viewModel) : base(viewModel) { }
    
    public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

public abstract class BasePage : ContentPage
{
    protected BasePage(BaseViewModel viewModel = null)
    {
        base.BindingContext = viewModel;
        this.NavigationService = Resolver.Resolve<INavigationService>();

        BindingContext.LoadDataCompleted += LoadDataCompleted;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.BindingContext.Initialize();
    }

    protected virtual void LoadDataCompleted(object sender, EventArgs e)
    {
        
    }

    public void BackButtonTapped(object sender, EventArgs e)
    {
        var handled = this.OnBackButtonPressed();
        if (!handled)
            this.NavigationService.CloseActivePage();
    }

    public new BaseViewModel BindingContext => (BaseViewModel)base.BindingContext;
    internal INavigationService NavigationService { get; }
}
