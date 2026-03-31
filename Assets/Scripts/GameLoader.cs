using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    private bool shouldLoadSavedData = false;

    private void Awake()
    {
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

        shouldLoadSavedData = true;

        SceneManagerController.Instance.LoadScene(data.scene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (shouldLoadSavedData)
        {
            SaveSystemV3.LoadGame();

            Debug.Log("Game state loaded");

            shouldLoadSavedData = false;
        }
    }
}