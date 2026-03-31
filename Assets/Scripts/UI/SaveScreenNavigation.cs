using UnityEngine;

public class SaveScreenNavigation : UINavigationBase
{
    [SerializeField] private PauseSelectionModel pauseSelectionModel;
    [SerializeField] private Transform defaultMenuRoot;

    private InputSystem_Actions controlsUI;
    private void Awake()
    {
        if (InputManagerController.Instance == null)
        {
            Debug.LogError("InputManagerController not found.");
            return;
        }

        controlsUI = InputManagerController.Instance.controls;

        if (controlsUI == null)
        {
            Debug.LogError("Controls not initialized.");
            return;
        }
    }
}
