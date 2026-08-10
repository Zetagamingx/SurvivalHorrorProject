using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseContainer;
    [SerializeField] private PauseSelectionModel pauseSelectionModel;
    [SerializeField] public InputActionReference pauseAction;
    [SerializeField] private SimpleStateMachine stateMachine;
    [SerializeField] private string mainSectionName;

    [SerializeField] private PlayerActionManager playerActionManager;

    private bool isOpen;

    private void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += TogglePause;
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= TogglePause;
        pauseAction.action.Disable();
    }

    private void Start()
    {
        pauseContainer.SetActive(false);
        isOpen = false;
    }
        
    public void OpenPauseMenu()
    {
        playerActionManager.DisableActions();

        isOpen = true;
        pauseContainer.SetActive(true);
        stateMachine.SetState(mainSectionName);

    }

    public void ClosePauseMenu()
    {
        playerActionManager.EnableActions();

        isOpen = false;
        stateMachine.SetState(mainSectionName);
        pauseContainer.SetActive(false);       
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        if (isOpen)
            ClosePauseMenu();
        else
            OpenPauseMenu();
    }
}
