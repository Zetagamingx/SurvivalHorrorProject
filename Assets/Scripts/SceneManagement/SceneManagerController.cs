using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameScene
{
    TitleScreen,
    IntroCinematic,
    IntroScene,
    HospitalScene,
}

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance;

    private Dictionary<GameScene, string> sceneMap;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("SceneManagerController is active.");

        sceneMap = new Dictionary<GameScene, string>
        {
            {GameScene.TitleScreen, "TitleScreen" },
            {GameScene.IntroCinematic, "IntroCinematic"},
            {GameScene.IntroScene, "IntroScene" },
            {GameScene.HospitalScene, "HospitalScene"}
        };
    }

    public void LoadScene(GameScene scene)
    {
        if (sceneMap.TryGetValue(scene, out string sceneName))
            SceneManager.LoadScene(sceneName);
        else
            Debug.LogWarning("Scene not found in sceneMap!");
    }

    public AsyncOperation LoadSceneAsync(GameScene scene)
    {
        Debug.Log($"Trying to load enum: {scene}");

        if (sceneMap.TryGetValue(scene, out string sceneName))
        {
            Debug.Log($"Loading scene name: {sceneName}");
            return SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene not found in sceneMap!");
            return null;
        }
    }

    public void RestartGame()
    {
        LoadScene(GameScene.TitleScreen);
        InputManagerController.Instance.ReinitializeControls();
        StartCoroutine(DestroyNextFrame());
    }

    private IEnumerator DestroyNextFrame()
    {
        yield return null;

        var objs = Object.FindObjectsByType<GameplayPersistent>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (var obj in objs)
        {
            Destroy(obj.gameObject);
        }
    }

    public GameScene GetCurrentGameScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        foreach (var pair in sceneMap)
        {
            if (pair.Value == currentSceneName)
                return pair.Key;
        }

        Debug.LogWarning("Current scene not found in sceneMap!");
        return GameScene.TitleScreen; // fallback
    }
}
