using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryMenuNavigationController : MonoBehaviour, InputSystem_Actions.IUIActions
{
    public static InventoryMenuNavigationController Instance;

    // Inventory UI References
    private GameObject mainCanvas;
    private GameObject inventoryMenu;
    private GameObject inventoryHUD;
    private GameObject mainMenuButtons;
    private GameObject greyedOutButtons;
    private GameObject inventoryPanel;
    //private GameObject videoPlayer;
    //private GameObject inventoryBackgroundVideo;
    //private InventoryVideoController inventoryVideoController;

    [SerializeField] List<Button> mainInventoryButtonList = new List<Button>();
    [SerializeField] List<Button> itemActionButtonList = new List<Button>();
    [SerializeField] List<Button> inventorySlotList = new List<Button>();
    private List<Button> currentButtonList;
    [SerializeField] int currentIndex = 0;

    private bool isInventoryOpen = false;
    private bool onInventoryMainMenu;
    private float navigateCooldown = 0.2f;
    private float lastNavigateTime;

    private InputSystem_Actions controlsUI => InputManagerController.Instance.controls;

    private void Awake()
    {
        Instance = this;
        controlsUI.UI.SetCallbacks(this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (controlsUI != null)
            controlsUI.UI.SetCallbacks(null); // unsubscribe before scene change
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCanvas = GameObject.Find("MainCanvas");

        if (mainCanvas == null)
        {
            Debug.Log("MainCanvas not found");
            return;
        }
        InventoryUI.Instance.OnSlotsCreated += GetInventorySlots;

        inventoryMenu = mainCanvas.transform.Find("InventoryMenu").gameObject;
        mainMenuButtons = inventoryMenu.transform.Find("MainMenuButtons").gameObject;
        inventoryHUD = inventoryMenu.transform.Find("InventoryHUD").gameObject;
        inventoryPanel = inventoryHUD.transform.Find("InventoryPanel").gameObject;
        greyedOutButtons = inventoryHUD.transform.Find("GreyedOutButtons").gameObject;

        //videoPlayer = mainCanvas.transform.Find("VideoPlayerController").gameObject;
        //inventoryBackgroundVideo = mainCanvas.transform.Find("InventoryBackgroundVideo").gameObject;
        //inventoryVideoController = videoPlayer.GetComponent<InventoryVideoController>();

        inventoryMenu.SetActive(true);
        mainMenuButtons.SetActive(true);
        inventoryHUD.SetActive(true);

        GetInventoryMainMenuButtons();
        GetItemActionButtons();
        GetInventorySlots();

        inventoryHUD.SetActive(false);
        mainMenuButtons.SetActive(false);
        inventoryMenu.SetActive(false);
    }

    public void InventoryAccess()
    {
        if (mainCanvas == null) return;

        if (!isInventoryOpen)
        {
            isInventoryOpen = true;
            //inventoryBackgroundVideo.SetActive(true);
            inventoryMenu.SetActive(true);
            //videoPlayer.SetActive(true);
            //inventoryVideoController.PlayVideo();
            onInventoryMainMenu = true;

            GoToInventoryMainMenu();
        }
        else if (onInventoryMainMenu)
        {
            isInventoryOpen = false;
            //inventoryBackgroundVideo?.SetActive(false);
            inventoryMenu?.SetActive(false);
            //videoPlayer?.SetActive(false);
            //inventoryVideoController?.StopVideo();
        }
    }
    private void GetInventorySlots()
    {
        Debug.Log("Im will get inventory slot buttons");
        inventorySlotList.Clear();
        foreach (Transform child in inventoryPanel.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
                inventorySlotList.Add(button);
        }
    }
    private void GetInventoryMainMenuButtons()
    {
        mainInventoryButtonList.Clear();
        foreach (Transform child in mainMenuButtons.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
                mainInventoryButtonList.Add(button);
        }
    }

    private void GetItemActionButtons()
    {
        itemActionButtonList.Clear();
        foreach (Transform child in inventoryHUD.transform.Find("ItemActionButtons"))
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
                itemActionButtonList.Add(button);
        }
    }

    public void GoToInventoryMainMenu()
    {
        onInventoryMainMenu = true;
        mainMenuButtons.SetActive(true);
        inventoryHUD.SetActive(false);
        currentButtonList = mainInventoryButtonList;
        currentIndex = 0;
        SelectButton(currentButtonList[currentIndex]);
    }

    public void GoToInventorySlotsMenu()
    {
        inventoryHUD.SetActive(true);
        greyedOutButtons.SetActive(true);
        currentButtonList = inventorySlotList;
        currentIndex = 0;
        SelectButton(currentButtonList[currentIndex]);
    }

    public void GoToItemActionMenu()
    {
        greyedOutButtons.SetActive(false);
        currentButtonList = itemActionButtonList;
        currentIndex = 0;
        SelectButton(currentButtonList[currentIndex]);
    }

    private void SelectButton(Button button)
    {
        button.Select();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || currentButtonList == null || currentButtonList.Count == 0)
            return;

        Vector2 navigation = context.ReadValue<Vector2>();

        if (Time.time - lastNavigateTime < navigateCooldown)
            return;

        if (navigation.y <= -0.5f) // Down
        {
            currentIndex = (currentIndex + 1) % currentButtonList.Count;
            SelectButton(currentButtonList[currentIndex]);
            lastNavigateTime = Time.time;
        }
        else if (navigation.y >= 0.5f) // Up
        {
            currentIndex = (currentIndex - 1 + currentButtonList.Count) % currentButtonList.Count;
            SelectButton(currentButtonList[currentIndex]);
            lastNavigateTime = Time.time;
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed || currentButtonList == null || currentButtonList.Count == 0)
            return;

        currentButtonList[currentIndex].onClick.Invoke();
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