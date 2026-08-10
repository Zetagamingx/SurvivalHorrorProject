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
        IInteract[] interactables = other.GetComponents<IInteract>();

        foreach (IInteract interact in interactables)
        {
            if (interact is Behaviour behaviour && behaviour.enabled)
            {
                CurrentInteract = interact;
                Debug.Log($"Assigned: {CurrentInteract}");
                return;
            }
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
