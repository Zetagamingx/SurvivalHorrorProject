using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenUINavigation : MonoBehaviour, InputSystem_Actions.IUIActions
{
    [SerializeField] private TitleSelectionModel titleSelectionModel;
    [SerializeField] private Transform defaultMenuRoot;

    private InputSystem_Actions controlsUI;

    private List<IUISelectable> currentSelectables = new List<IUISelectable>();
    private int currentIndex = 0;
    
    private float navigateCooldown = 0.2f;
    private float lastNavigateTime;

    private void Start()
    {
        // --- Initialize Input ---
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

        controlsUI.UI.SetCallbacks(this);

        // --- Initialize first menu ---
        InitializeFirstMenu();
    }

    private void OnEnable()
    {
        if (titleSelectionModel != null)
            titleSelectionModel.OnSectionChanged += HandleSectionChanged;
    }
    private void OnDisable()
    {
        if (controlsUI != null)
            controlsUI.UI.SetCallbacks(null);

        if (titleSelectionModel != null)
            titleSelectionModel.OnSectionChanged -= HandleSectionChanged;
    }

    private void InitializeFirstMenu()
    {
        if (defaultMenuRoot == null)
        {
            Debug.LogError("[NAV] Default menu root not assigned.");
            return;
        }

        SetActiveMenu(defaultMenuRoot);
    }


    public void SetActiveMenu(Transform menuRoot)
    {
        if (menuRoot == null)
        {
            Debug.LogError("[NAV] SetActiveMenu called with NULL");
            return;
        }

        currentSelectables.Clear();

        var selectables = menuRoot
            .GetComponentsInChildren<MonoBehaviour>(true)
            .OfType<IUISelectable>();

        foreach (var selectable in selectables)
        {
            currentSelectables.Add(selectable);
            Debug.Log($"[NAV] Found selectable: {((MonoBehaviour)selectable).name}");
        }

        Debug.Log($"[NAV] Total selectables: {currentSelectables.Count}");

        currentIndex = 0;

        if (currentSelectables.Count > 0)
        {
            currentSelectables[currentIndex].OnSelected();
        }
    }

    private void HandleSectionChanged()
    {
        // Find the active child section
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                SetActiveMenu(child);
                break;
            }
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || currentSelectables.Count == 0)
            return;

        if (Time.time - lastNavigateTime < navigateCooldown)
            return;

        Vector2 navigation = context.ReadValue<Vector2>();

        int previousIndex = currentIndex;

        if (navigation.y <= -0.5f) // Down
        {
            currentIndex = (currentIndex + 1) % currentSelectables.Count;
        }
        else if (navigation.y >= 0.5f) // Up
        {
            currentIndex = (currentIndex - 1 + currentSelectables.Count) % currentSelectables.Count;
        }
        else
        {
            return;
        }

        currentSelectables[previousIndex].OnDeselected();
        currentSelectables[currentIndex].OnSelected();

        lastNavigateTime = Time.time;
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed || currentSelectables.Count == 0)
            return;

        currentSelectables[currentIndex].OnSubmit();
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