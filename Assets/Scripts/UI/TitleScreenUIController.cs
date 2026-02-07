using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
public class TitleScreenUIController : MonoBehaviour, InputSystem_Actions.IUIActions

{
    public GameObject titleMainMenu;
    public GameObject titleSettingsMenu;
    public GameObject titleNameAndEkg;

    [SerializeField] List<Button> titleButtonList = new List<Button>();
    [SerializeField] List<Button> settingsButtonList = new List<Button>();
    private List<Button> currentButtonList;
    [SerializeField] int currentIndex = 0;

    private InputSystem_Actions controlsUI => InputManagerController.Instance.controls;


    private float navigateCooldown = 0.2f;
    private float lastNavigateTime;

    private void Awake()
    {
        
        controlsUI.UI.SetCallbacks(this);
        GetTitleMainMenuButtons();
        GetSettingsMenuButtons();
    }
    private void OnDestroy()
    {
        if (controlsUI != null)
            controlsUI.UI.SetCallbacks(null); //  unsubscribe before scene change
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }
    public void GetTitleMainMenuButtons()
    {
        titleButtonList.Clear();
        foreach(Transform child in titleMainMenu.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                titleButtonList.Add(button);
            }    
        }
    }

    public void GetSettingsMenuButtons()
    {
        settingsButtonList.Clear();
        foreach (Transform child in titleSettingsMenu.transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                settingsButtonList.Add(button);
            }
        }
    }

    public void ActivateMainMenu()
    {
        titleMainMenu.SetActive(true);
        titleSettingsMenu.SetActive(false);

        currentButtonList = titleButtonList;
        currentIndex = 0;
        SelectButton(currentButtonList[currentIndex]);
    }

    public void ActivateSettingsMenu()
    {
        titleMainMenu.SetActive(false);
        titleSettingsMenu.SetActive(true);

        currentButtonList = settingsButtonList;
        currentIndex = 0;
        SelectButton(currentButtonList[currentIndex]);
    }

    void SelectButton(Button button)
    {
        button.Select();
    }
    void Start()
    {
        ActivateMainMenu();
    }

    // Update is called once per frame
    public void Update()
    {

        
        
    }
    
    public void OnCancel(InputAction.CallbackContext context)
    {
    }

    public void OnClick(InputAction.CallbackContext context)
    {
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        if (!context.performed || currentButtonList == null || currentButtonList.Count == 0)
            return;

        Vector2 navigation = context.ReadValue<Vector2>();

        if (Time.time - lastNavigateTime < navigateCooldown)
            return;

        

        // Only change index and select if cooldown is passed
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



    public void OnPoint(InputAction.CallbackContext context)
    {
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (!context.performed || currentButtonList == null || currentButtonList.Count == 0)
            return;

        currentButtonList[currentIndex].onClick.Invoke();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
    }

}
