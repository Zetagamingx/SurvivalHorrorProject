[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null || quantity <= 0;
}
