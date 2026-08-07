using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenUINavigation : UINavigationBase
{
    [SerializeField] private TitleSelectionModel titleSelectionModel;
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

    private void Start()
    {
        InitializeFirstMenu();
    }

    private void OnEnable()
    {
        StartCoroutine(SetupInputNextFrame());

        if (titleSelectionModel != null)
            titleSelectionModel.OnSectionChanged += HandleSectionChanged;    

        Debug.Log("Controls asset: " + controlsUI);
        Debug.Log("UI map enabled: " + controlsUI.UI.enabled);
    }
    private void OnDisable()
    {
        UIInputRouter.Instance.ClearOwner(this);

        if (titleSelectionModel != null)
            titleSelectionModel.OnSectionChanged -= HandleSectionChanged;
    }

    private IEnumerator SetupInputNextFrame()
    {
        yield return null; // wait 1 frame

        UIInputRouter.Instance.SetOwner(this);
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
            //Debug.Log($"[NAV] Found selectable: {((MonoBehaviour)selectable).name}");
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

    public override void OnSubmit(InputAction.CallbackContext context)
    {
        base.OnSubmit(context);

        // extra title logic here
    }
}