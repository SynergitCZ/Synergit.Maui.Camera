namespace Synergit.Maui.Camera.Test.Services;

public interface INavigationService
{
    Page ActivePage { get; }
    Task ShowPage<TPage>() where TPage : Page;
    Task ShowPage<TPage>(Guid id, Dictionary<string, object> otherParams = null) where TPage : Page;
    Task ShowPageEx<TPage>() where TPage : Page;
    Task ShowPageEx<TPage>(Guid id, Dictionary<string, object> otherParams = null) where TPage : Page;
    Task CloseActiveAndShowPageEx<TPage>() where TPage : Page;
    Task CloseActiveAndShowPageEx<TPage>(Guid id, Dictionary<string, object> otherParams = null) where TPage : Page;   
    Task CloseAllAndShowPage<TPage>() where TPage : BasePage;
    Task CloseActivePage();
    void CloseAllButActivePage();
}
