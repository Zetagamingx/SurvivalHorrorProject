using UnityEngine;
using UnityEngine.InputSystem;

public class SaveStationController : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject backgroundImage;
    [SerializeField] private GameObject sectionContainer;
    [SerializeField] private PauseSelectionModel pauseSelectionModel;
    private bool isPlayerInside = false;

    public bool IsPlayerInside => isPlayerInside;
    //public GameObject saveScreen;

    public string InteractionPrompt => "Press E to save";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player can save");
            isPlayerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player cannot save");
            isPlayerInside = false;
        }
    }

    public void Interact()
    {
        if (isPlayerInside)
        {
            ActivateSaveScreen();
        }
    }
    public void ActivateSaveScreen()
    {
        if (pauseSelectionModel == null)
        {
            Debug.LogError("PauseSelectionModel not assigned");
            return;
        }

        InGameMenuController.Instance.OpenMenu();
        pauseSelectionModel.EnterSaveSection();
    }


}
