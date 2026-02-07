using UnityEngine;

public class PersistantPlayer : MonoBehaviour
{
    public static PersistantPlayer Instance;
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
