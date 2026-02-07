using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryAccessController : MonoBehaviour
{
    public static InventoryAccessController Instance;

    //private GameObject videoPlayer;
    private GameObject inventoryMenu;
    private GameObject mainCanvas;
    //private GameObject inventoryBackgroundVideo;
    //private InventoryVideoController inventoryVideoController;
    private GameObject inventoryHUD;
    private GameObject mainMenuButtons;
    private GameObject greyedOutButtons;
    
    
    public bool isInventoryOpen = false;

    public void Awake()
    {
        Instance = this;
        isInventoryOpen = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        mainCanvas = GameObject.Find("MainCanvas");

        if (mainCanvas == null)
        {
            Debug.Log("MainCanvas not found");
            return;
        }

        inventoryMenu = mainCanvas.transform.Find("InventoryMenu").gameObject;
        mainMenuButtons = inventoryMenu.transform.Find("MainMenuButtons").gameObject;
        inventoryHUD = inventoryMenu.transform.Find("InventoryHUD").gameObject;
        greyedOutButtons = inventoryHUD.transform.Find("GreyedOutButtons").gameObject;
        
        //videoPlayer = mainCanvas.transform.Find("VideoPlayerController").gameObject;
        //inventoryBackgroundVideo = mainCanvas.transform.Find("InventoryBackgroundVideo").gameObject;
        //inventoryVideoController = videoPlayer.GetComponent<InventoryVideoController>();
     


    }
    public void InventoryAccess ()
    {
        if (mainCanvas == null) return;

        if (!isInventoryOpen)
        {
            isInventoryOpen = true;
            //inventoryBackgroundVideo.SetActive(true);
            inventoryMenu.SetActive(true);
            
            //videoPlayer.SetActive(true);
            //inventoryVideoController.PlayVideo();
            return;
        }
        else if (isInventoryOpen)
        {
            isInventoryOpen = false;
            //inventoryBackgroundVideo?.SetActive(false);
            inventoryMenu?.SetActive(false);
            ///videoPlayer?.SetActive(false);
            //inventoryVideoController?.StopVideo();
            return;
        }

    }

  
}
