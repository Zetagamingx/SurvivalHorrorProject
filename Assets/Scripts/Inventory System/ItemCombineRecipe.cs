using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Combine Recipe")]
public class ItemCombineRecipe : ScriptableObject
{
    public ItemData inputA;
    public ItemData inputB;
    public ItemData result;
}
