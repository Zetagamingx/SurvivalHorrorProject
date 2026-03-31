using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UINavigationBase : MonoBehaviour, InputSystem_Actions.IUIActions
{
    protected List<IUISelectable> currentSelectables = new List<IUISelectable>();
    protected int currentIndex = 0;

    protected float navigateCooldown = 0.2f;
    protected float lastNavigateTime;

    public virtual void OnNavigate(InputAction.CallbackContext context)
    {
        Debug.Log("Selectable count: " + currentSelectables.Count);

        if (!context.performed || currentSelectables.Count == 0)
            return;

        if (Time.unscaledTime - lastNavigateTime < navigateCooldown)
            return;

        Vector2 navigation = context.ReadValue<Vector2>();

        int previousIndex = currentIndex;

        if (navigation.y <= -0.5f)
            currentIndex = (currentIndex + 1) % currentSelectables.Count;
        else if (navigation.y >= 0.5f)
            currentIndex = (currentIndex - 1 + currentSelectables.Count) % currentSelectables.Count;
        else
            return;

        currentSelectables[previousIndex].OnDeselected();
        currentSelectables[currentIndex].OnSelected();

        lastNavigateTime = Time.unscaledTime;
    }

    public virtual void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed || currentSelectables.Count == 0)
            return;

        currentSelectables[currentIndex].OnSubmit();
    }

    // leave others empty
    public virtual void OnCancel(InputAction.CallbackContext context) { }
    public void OnClick(InputAction.CallbackContext context) { }
    public void OnMiddleClick(InputAction.CallbackContext context) { }
    public void OnPoint(InputAction.CallbackContext context) { }
    public void OnRightClick(InputAction.CallbackContext context) { }
    public void OnScrollWheel(InputAction.CallbackContext context) { }
    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }
    public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
}