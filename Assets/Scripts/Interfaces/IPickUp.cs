using UnityEngine.UIElements;

public interface IPickUp
{
    string ItemObtainedPrompt { get; }

    ItemData Data { get; }
    int Quantity { get; }
}
