namespace Synergit.Maui.Camera.Test.Services;

public class NavigationService : INavigationService
{
    readonly IServiceProvider serviceProvider;

    protected static INavigation Navigation => App.Current?.MainPage?.Navigation;

    public Page ActivePage => Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];

    public NavigationService(IServiceProvider services) => this.serviceProvider = services;

    public async Task ShowPage<TPage>(Guid id, Dictionary<string, object> otherParams = null) where TPage : Page
    {
        try
        {
            var page = serviceProvider.GetService<TPage>();

            if ((id != Guid.Empty) && page.BindingContext is BaseViewModel vm)
            {
                vm.Prepare(id, otherParams);
            }

            await Navigation.PushAsync(page, true);
        }
        catch (Exception ex)
        {
            await App.Current.MainPage.DisplayAlert("Error", ex.InnerException?.Message ?? ex.Message, "Ok");
        }
    }

    public async Task ShowPage<TPage>() where TPage : Page
    {
        await this.ShowPage<TPage>(Guid.Empty);
    }

    public async Task ShowPageEx<TPage>(Guid id, Dictionary<string, object> otherParams = null) where TPage : Page
    {
        var currentViewModel = BaseViewModel.CurrentViewModel;
        if (currentViewModel == null)
        {
            await this.ShowPage<TPage>(id, otherParams);
        }
        else
        {
            currentViewModel.IsBusy = true;
            try
            {
                await this.ShowPage<TPage>(id, otherParams);
            }
            finally
            {
                currentViewModel.IsBusy = false;
            }
        }
    }

    public async Task ShowPageEx<TPage>() where TPage : Page
    {
        await this.ShowPageEx<TPage>(Guid.Empty);
    }

    public async Task CloseActiveAndShowPageEx<TPage>(Guid id, Dictionary<string, object> otherParams = null) where TPage : Page
    {
        var currentPage = Navigation.NavigationStack[Navigation.NavigationStack.Count-1];

        var currentViewModel = BaseViewModel.CurrentViewModel;
        if (currentViewModel == null)
        {
            await this.ShowPage<TPage>(id, otherParams);
        }
        else
        {
            currentViewModel.IsBusy = true;
            try
            {
                await this.ShowPage<TPage>(id, otherParams);
            }
            finally
            {
                currentViewModel.IsBusy = false;
            }
        }

        Navigation.RemovePage(currentPage);
    }

    public async Task CloseActiveAndShowPageEx<TPage>() where TPage : Page
    {
        await this.CloseActiveAndShowPageEx<TPage>(Guid.Empty);
    }

    public async Task CloseActivePage()
    {
        await Navigation.PopAsync();
    }

    public void CloseAllButActivePage()
    {
        BaseViewModel.ResetCaller(false);
        for (var i = Navigation.NavigationStack.Count - 2; i >= 0; i--)
        {
            Navigation.RemovePage(Navigation.NavigationStack[i]);
        }
    }

    public async Task CloseAllAndShowPage<TPage>() where TPage : BasePage
    {
        BaseViewModel.ResetCaller(true);
        var page = serviceProvider.GetService<TPage>() ?? throw new InvalidOperationException($"Unable to resolve type {typeof(TPage).FullName}");
        await Navigation.PopToRootAsync(false);
        await Navigation.PushAsync(page, false);
        Navigation.RemovePage(Navigation.NavigationStack[0]);
    }

}
