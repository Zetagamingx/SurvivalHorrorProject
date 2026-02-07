using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameScene
{
    TitleScreen,
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
