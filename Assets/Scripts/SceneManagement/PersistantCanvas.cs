using UnityEngine;

public class PersistantCanvas : MonoBehaviour
{
    public static PersistantCanvas Instance;

    public void Awake()
    {

        Debug.Log("another test");
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyForTitle()
    {
        Instance = null; // important to avoid stale reference
        Destroy(gameObject);
    }
}
