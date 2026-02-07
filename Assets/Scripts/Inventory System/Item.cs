using UnityEngine;

public abstract class Item
{
    public ItemData Data { get; private set; }

    protected Item(ItemData data)
    {
        Data = data;
    }

    public abstract void Use(); // runtime behavior
}
