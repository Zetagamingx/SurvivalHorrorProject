public interface IObtainable : IInteractable
{
    ItemData Data { get; }
    int Quantity { get; }
}
