using UnityEngine;

public class PersistentGlobalVolume : MonoBehaviour
{
    public static PersistentGlobalVolume Instance;
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
