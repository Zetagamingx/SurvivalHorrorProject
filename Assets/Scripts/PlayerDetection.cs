using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public IInteractable CurrentInteractable { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
            CurrentInteractable = interactable;

        Debug.Log($"This object is:{interactable}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() == CurrentInteractable)
            CurrentInteractable = null;
    }

    public void ClearInteractable()
    {
        CurrentInteractable = null;
    }
}