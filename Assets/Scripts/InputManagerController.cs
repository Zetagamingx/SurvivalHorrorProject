using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class InputManagerController : MonoBehaviour
{
    public static InputManagerController Instance;
    public InputSystem_Actions controls;
    public bool shouldMove = true;

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
        if (shouldMove == value)
            return;

        shouldMove = value;

        if (shouldMove)
            controls.Player.Enable();
        else
            controls.Player.Disable();
    }
}
