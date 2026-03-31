public interface IMenuController
{
    void Show();
    void Hide();
    UINavigationBase GetNavigation();
    void OnMenuOpened();
}