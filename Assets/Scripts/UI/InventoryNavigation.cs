using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;

public class InventoryNavigation : MonoBehaviour, InputSystem_Actions.IUIActions
{
    [Header("UI References")]
    public GameObject inventoryRoot;        // Entire inventory UI
    public GameObject buttonContainer;      // Parent of 3 buttons

    [SerializeField] private List<Button> buttonList = new List<Button>();
    public int currentIndex = 0;

    private InputSystem_Actions controls;

    public static InventoryNavigation Instance;

    private void Awake()
    {
        Instance = this;
        controls = new InputSystem_Actions();
        controls.UI.SetCallbacks(this);
        controls.UI.Disable();
    }

    private void Start()
    {
        GetButtonsFromContainer();
    }

    private void OnDestroy()
    {
        controls.UI.SetCallbacks(null);
        controls.Dispose();
    }

    private void GetButtonsFromContainer()
    {
        buttonList.Clear();

        foreach (Transform child in buttonContainer.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
                buttonList.Add(btn);
        }
    }

    public void ToggleInventory()
    {
        bool isNowActive = !inventoryRoot.activeSelf;
        inventoryRoot.SetActive(isNowActive);

        if (isNowActive)
        {
            controls.UI.Enable(); //  Enable input map
            currentIndex = 0;
            SelectButton(buttonList[currentIndex]);
        }
        else
        {
            controls.UI.Disable(); //  Disable when closing
        }
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || buttonList.Count == 0)
            return;

        Vector2 direction = context.ReadValue<Vector2>();

        if (direction.y > 0.5f)
            currentIndex = (currentIndex - 1 + buttonList.Count) % buttonList.Count;
        else if (direction.y < -0.5f)
            currentIndex = (currentIndex + 1) % buttonList.Count;

        SelectButton(buttonList[currentIndex]);
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed && buttonList.Count > 0)
        {
            buttonList[currentIndex].onClick.Invoke();
        }
    }


    private void SelectButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
        button.Select();
        Debug.Log($"[Select] Selected {button.name}");
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            Debug.Log("[EventSystem] Currently selected: " + EventSystem.current.currentSelectedGameObject.name);
        }
    }

    private IEnumerator ForceHighlightCoroutine(Button button)
    {
        // Wait two frames for Unity to fully update selection state
        yield return null;
        yield return null;

        EventSystem.current.SetSelectedGameObject(null);

        foreach (var btn in buttonList)
        {
            if (btn != button)
            {
                ExecuteEvents.Execute(btn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
            }
        }

        EventSystem.current.SetSelectedGameObject(button.gameObject);
        button.OnSelect(null);
        ExecuteEvents.Execute(button.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);

        Debug.Log($"[Highlight] Final force highlight: {button.name}");
    }
    // Empty interface methods
    public void OnCancel(InputAction.CallbackContext context) { }
    public void OnClick(InputAction.CallbackContext context) { }
    public void OnMiddleClick(InputAction.CallbackContext context) { }
    public void OnPoint(InputAction.CallbackContext context) { }
    public void OnRightClick(InputAction.CallbackContext context) { }
    public void OnScrollWheel(InputAction.CallbackContext context) { }
    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }
    public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
}