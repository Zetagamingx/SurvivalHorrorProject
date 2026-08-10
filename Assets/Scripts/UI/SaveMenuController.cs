using UnityEngine;

public class SaveMenuController : MonoBehaviour
{
    [SerializeField] private GameObject saveContainer;
    [SerializeField] private PlayerActionManager playerActionManager;

    private void Start()
    {
        saveContainer.SetActive(false);
    }


    public void EnableSaveScreen()
    {
        playerActionManager.DisableActions();
        saveContainer.SetActive(true);
    }

    public void DisableSaveScreen()
    {
        saveContainer.SetActive(false);
        playerActionManager.EnableActions();
    }
}
