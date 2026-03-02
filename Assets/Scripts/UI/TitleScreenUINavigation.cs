using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TitleScreenUINavigation : MonoBehaviour, InputSystem_Actions.IUIActions
{
    private List<Button> currentButtons = new List<Button>();
    private int currentIndex = 0;

    private InputSystem_Actions controlsUI => InputManagerController.Instance.controls;

    private float navigateCooldown = 0.2f;
    private float lastNavigateTime;

    private void OnEnable()
    {
        if (controlsUI != null)
            controlsUI.UI.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (controlsUI != null)
            controlsUI.UI.SetCallbacks(null);
    }

    /// <summary>
    /// Call this whenever the active UI section changes.
    /// Pass the transform of the active section root.
    /// </summary>
    public void SetActiveMenu(Transform menuRoot)
    {
        if (menuRoot == null)
        {
            Debug.LogError("SetActiveMenu called with NULL transform");
            return;
        }

        Debug.Log($"[NAV] Setting active menu: {menuRoot.name}");

        currentButtons.Clear();

        foreach (Transform child in menuRoot)
        {
            Debug.Log($"[NAV] Checking child: {child.name}");

            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                Debug.Log($"[NAV] Found button: {child.name}");
                currentButtons.Add(button);
            }
        }

        Debug.Log($"[NAV] Total buttons found: {currentButtons.Count}");

        currentIndex = 0;

        if (currentButtons.Count > 0)
            SelectButton(currentButtons[currentIndex]);
    }

    private void SelectButton(Button button)
    {
        button.Select();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || currentButtons.Count == 0)
            return;

        Vector2 navigation = context.ReadValue<Vector2>();

        if (Time.time - lastNavigateTime < navigateCooldown)
            return;

        if (navigation.y <= -0.5f) // Down
        {
            currentIndex = (currentIndex + 1) % currentButtons.Count;
            SelectButton(currentButtons[currentIndex]);
            lastNavigateTime = Time.time;
        }
        else if (navigation.y >= 0.5f) // Up
        {
            currentIndex = (currentIndex - 1 + currentButtons.Count) % currentButtons.Count;
            SelectButton(currentButtons[currentIndex]);
            lastNavigateTime = Time.time;
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed || currentButtons.Count == 0)
            return;

        currentButtons[currentIndex].onClick.Invoke();
    }

    // Unused interface methods
    public void OnCancel(InputAction.CallbackContext context) { }
    public void OnClick(InputAction.CallbackContext context) { }
    public void OnMiddleClick(InputAction.CallbackContext context) { }
    public void OnPoint(InputAction.CallbackContext context) { }
    public void OnRightClick(InputAction.CallbackContext context) { }
    public void OnScrollWheel(InputAction.CallbackContext context) { }
    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }
    public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
}