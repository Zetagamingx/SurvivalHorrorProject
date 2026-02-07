using UnityEngine;

public class InputManagerController : MonoBehaviour
{
    public static InputManagerController Instance;
    public InputSystem_Actions controls;

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
}
