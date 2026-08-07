using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameLoader : MonoBehaviour
{
    public static GameLoader Instance;

    private bool shouldLoadSavedData = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ContinueGame()
    {
        SaveData data = SaveSystemV3.LoadSaveDataOnly();

        if (data == null)
        {
            Debug.LogWarning("No save file found");
            return;
        }

        InputManagerController.Instance.SetLoadingState(true);

        shouldLoadSavedData = true;

        SceneManagerController.Instance.LoadScene(data.scene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (shouldLoadSavedData)
        {
            StartCoroutine(LoadGameRoutine());
            shouldLoadSavedData = false;
        }
    }

    private IEnumerator LoadGameRoutine()
    {
        SaveSystemV3.isLoading = true;

        SaveSystemV3.LoadGame();

        //  Wait for physics step to finish
        yield return new WaitForFixedUpdate();

        var player = GameObject.FindGameObjectWithTag("Player");
        var rb = player.GetComponentInChildren<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        SaveSystemV3.isLoading = false;

        var controller = player.GetComponent<PlayerController>();
        if (controller != null)
        {
            InputManagerController.Instance.controls.Player.SetCallbacks(controller);
            Debug.Log("FORCED REBIND AFTER LOAD");
        }

        InputManagerController.Instance.SetLoadingState(false);

        Debug.Log("Game state loaded");
    }
}