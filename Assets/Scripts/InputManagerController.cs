using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InputManagerController : MonoBehaviour
{
    public static InputManagerController Instance;
    public InputSystem_Actions controls;
    public bool shouldMove = true;
    private bool isLoading;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        controls = new InputSystem_Actions();
        controls.Enable();
    }

    public void SetPlayerMovement(bool value)
    {
        shouldMove = value;

        if (controls == null)
            return;

        if (isLoading)
            return;

        if (shouldMove)
            controls.Player.Enable();
        else
            controls.Player.Disable();
    }

    public void SetLoadingState(bool value)
    {
        isLoading = value;

        if (controls == null)
            return;

        if (value)
        {
            controls.Player.Disable();
        }
        else
        {
            if (shouldMove)
                controls.Player.Enable();
        }
    }

    public void ReinitializeControls()
    {
        if (controls != null)
        {
            controls.Disable();
        }

        controls = new InputSystem_Actions();
        controls.Enable();

        // Restore state
        if (!shouldMove)
            controls.Player.Disable();
    }
}
