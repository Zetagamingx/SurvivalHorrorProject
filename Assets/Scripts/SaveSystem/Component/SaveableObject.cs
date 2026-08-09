using UnityEngine;

public class SaveableObject : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private Behaviour componentToTrack;

    public string GetID()
    {
        return id;
    }

    public bool GetState()
    {
        return gameObject.activeSelf;
    }

    public void ApplyState(bool state)
    {
        gameObject.SetActive(state);
    }

    public bool HasComponent()
    {
        return componentToTrack != null;
    }

    public bool GetComponentState()
    {
        return componentToTrack != null && componentToTrack.enabled;
    }

    public void ApplyComponentState(bool state)
    {
        if (componentToTrack != null)
            componentToTrack.enabled = state;
    }

    private void OnEnable()
    {
        SaveRegistry.Register(this);
    }

    private void OnDisable()
    {
        SaveRegistry.Unregister(this);
    }
}