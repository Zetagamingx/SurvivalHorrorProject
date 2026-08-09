using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public IInteract CurrentInteract { get; private set; }

    public void ClearCurrentInteract()
    {
        CurrentInteract = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteract>(out var interact))
        {
            CurrentInteract = interact;
            Debug.Log($"Assigned: {CurrentInteract}");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<IInteract>(out var interact) && interact == CurrentInteract)
        {
            CurrentInteract = null;
        }

    }
}
