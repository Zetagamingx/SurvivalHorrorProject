using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private IMenuController currentMenu;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OpenMenu(IMenuController menu)
    {
        if (menu == null)
            return;

        if (currentMenu != null)
        {
            currentMenu.Hide();
        }

        currentMenu = menu;
        currentMenu.Show();

        var navigation = menu.GetNavigation();
        UIInputRouter.Instance.SetOwner(navigation);
    }

    public void CloseCurrentMenu()
    {
        if (currentMenu == null)
            return;

        currentMenu.Hide();
        currentMenu = null;

        UIInputRouter.Instance.SetOwner(null);
    }
}