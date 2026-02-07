using UnityEngine;

public class PersistantThirdCamera : MonoBehaviour
{
    public static PersistantThirdCamera Instance;
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
