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
        shouldLoadSavedData = true;
        SceneManagerController.Instance.LoadScene(GameScene.IntroScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (shouldLoadSavedData && scene.name == "IntroScene")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 savedPos = SaveSystem.LoadPlayerPosition();
                player.transform.position = savedPos;
                Debug.Log("Player position loaded: " + savedPos);
            }
            else
            {
                Debug.LogWarning("Player not found in scene to apply loaded position.");
            }

            shouldLoadSavedData = false;
        }
    }
}