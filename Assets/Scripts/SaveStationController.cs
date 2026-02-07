using UnityEngine;
using UnityEngine.InputSystem;

public class SaveStationController : MonoBehaviour, IInteractable
{
    public GameObject saveScreen;
    private bool isPlayerInside = false;

    public bool IsPlayerInside => isPlayerInside;

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
        if (!saveScreen.activeSelf)
        {
            saveScreen.SetActive(true);
            Time.timeScale = 0f;
            
        }
    }

  
}
