using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuNavigation : UINavigationBase
{
    [SerializeField] private PauseSelectionModel pauseSelectionModel;
    [SerializeField] private Transform sectionContainer;
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
        //InitializeFirstMenu();
    }

    private void OnEnable()
    {
        StartCoroutine(SetupInputNextFrame());

        if (pauseSelectionModel != null)
            pauseSelectionModel.OnPauseSectionChanged += HandleSectionChanged;

        InitializeFirstMenu();

        Debug.Log("Controls asset: " + controlsUI);
        Debug.Log("UI map enabled: " + controlsUI.UI.enabled);
    }

    private void OnDisable()
    {
        if (UIInputRouter.Instance != null)
        {
            UIInputRouter.Instance.ClearOwner(this);
        }

        if (pauseSelectionModel != null)
            pauseSelectionModel.OnPauseSectionChanged -= HandleSectionChanged;
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

        pauseSelectionModel.ResetToDefault();
        SetActiveMenu(defaultMenuRoot);
    }

    public void SetActiveMenu(Transform menuRoot)
    {
        Debug.Log($"[NAV] SetActiveMenu called with: {menuRoot.name}");

        Debug.Log($"[NAV] Searching inside: {menuRoot.name}");

        foreach (Transform t in menuRoot.GetComponentsInChildren<Transform>(true))
        {
            Debug.Log($"  Child: {t.name}");
        }

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
        string sectionName = pauseSelectionModel.CurrentSection;

        Debug.Log("=== HandleSectionChanged ===");
        Debug.Log("Looking for section: " + sectionName);

        if (defaultMenuRoot == null)
        {
            Debug.LogError("defaultMenuRoot is NULL");
            return;
        }

        Debug.Log("DefaultMenuRoot is: " + defaultMenuRoot.name);

        foreach (Transform child in defaultMenuRoot)
        {
            Debug.Log("Child under defaultMenuRoot: " + child.name);
        }

        Transform target = sectionContainer.Find(sectionName);

        if (target == null)
        {
            Debug.LogError($"Section not found: {sectionName}");
            return;
        }

        SetActiveMenu(target);
    }

    public override void OnSubmit(InputAction.CallbackContext context)
    {
        base.OnSubmit(context);

        // extra title logic here
    }
}