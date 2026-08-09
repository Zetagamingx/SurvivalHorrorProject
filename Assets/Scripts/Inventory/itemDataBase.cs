using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<ItemData> allItems = new();

    public IReadOnlyList<ItemData> AllItems => allItems;

    public ItemData GetItemByName(string itemName)
    {
        return allItems.Find(item => item.ItemName == itemName);
    }

    public ItemData GetItemByID(int id)
    {
        return allItems.Find(item => item.ID == id);
    }

    public bool Contains(ItemData item)
    {
        return allItems.Contains(item);
    }
}