using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public static LoadingScreenController Instance { get; private set;}
    public GameObject loadingRoot; // Parent object for black screen + animation
    public float delayBeforeHide = 3f;
    private FogController FogController;

    public bool isLoading = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FogController = GetComponent<FogController>();
        if (loadingRoot != null)
        {
            FogController.DisableFog();
            loadingRoot.SetActive(true);
            StartCoroutine(HideLoadingScreenAfterDelay());
        }

        Time.timeScale = 1f; // Freeze time
        BlockInput(true);
    }

    public IEnumerator HideLoadingScreenAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeHide); // Use unscaled time!

        loadingRoot.SetActive(false);
        FogController?.EnableFog();
        Time.timeScale = 1f;
        BlockInput(false);
        isLoading = false;
    }

    private void BlockInput(bool block)
    {
        if (InputManagerController.Instance != null)
        {
            if (block)
                InputManagerController.Instance.controls.Disable();
            else
                InputManagerController.Instance.controls.Enable();
        }
    }

    public void RoomTransition()
    {
        if (loadingRoot != null)
        {
            FogController.DisableFog();
            loadingRoot.SetActive(true);
            StartCoroutine(HideLoadingScreenAfterDelay());
        }

        
        BlockInput(true);
    }
}
