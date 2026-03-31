using UnityEngine;

public class UIInputRouter : MonoBehaviour
{
    public static UIInputRouter Instance;

    private InputSystem_Actions controls;

    private InputSystem_Actions.IUIActions currentOwner;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetOwner(InputSystem_Actions.IUIActions owner)
    {

        Debug.Log("SET OWNER CALLED: " + owner);

        var inputManager = InputManagerController.Instance;

        if (inputManager == null)
        {
            Debug.LogError("InputManagerController is NULL");
            return;
        }

        var controls = inputManager.controls;

        if (controls == null)
        {
            Debug.LogError("Controls NULL");
            return;
        }

        controls.UI.SetCallbacks(null);

        currentOwner = owner;

        if (owner != null)
        {
            controls.UI.SetCallbacks(owner);
            controls.UI.Enable();

            Debug.Log("UI Enabled AFTER SET: " + controls.UI.enabled);
        }
    }

    public void ClearOwner(InputSystem_Actions.IUIActions owner)
    {
        if (currentOwner != owner)
            return;

        if (controls == null)
        {
            Debug.LogWarning("ClearOwner: controls is null");
            return;
        }

        // Optional: extra safety if you want
        try
        {
            controls.UI.SetCallbacks(null);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"ClearOwner failed: {e.Message}");
        }

        currentOwner = null;
    }
}