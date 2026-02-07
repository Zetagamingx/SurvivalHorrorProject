using UnityEngine;

public class PersistantMainCamera : MonoBehaviour
{
    public static PersistantMainCamera Instance { get; private set; }
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
