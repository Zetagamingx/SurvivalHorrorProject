using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItems;

    public ItemData GetItemByName(string name)
    {
        return allItems.Find(item => item.itemName == name);
    }
}
