using UnityEngine;

public class PersistantPlayer : MonoBehaviour
{
    public Vector3 lastPos;

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

    void Start()
    {
        Debug.Log("PLAYER START POS: " + transform.position);
    }

    void Update()
    {
        Debug.Log("PLAYER UPDATE POS: " + transform.position);
    }

    void LateUpdate()
    {
        if (lastPos != transform.position)
        {
            Debug.Log($"POSITION CHANGED from {lastPos}  {transform.position}");
        }

        lastPos = transform.position;
    }
}
