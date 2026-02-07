public interface IInteractable
{
    void Interact();
    string InteractionPrompt { get; } // Optional: for UI
}
