using UnityEngine;
using UnityEngine.InputSystem;

public class SaveStationActivator : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject saveContainer;
    [SerializeField] private SaveMenuController saveMenuController;
        
    public string InteractionPrompt => "Press E to save";

    public void Interact()
    {
        saveMenuController.EnableSaveScreen();
    }
   
}
