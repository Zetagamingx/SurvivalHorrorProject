using UnityEngine;

public enum ItemCategory
{
    key,
    equipment,
    weapons,
    consumable,
    puzzlePiece
}

[CreateAssetMenu(menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string ItemName;
    public int ID;
    public Sprite Icon;

    [Header("Inventory")]
    public bool IsStackable = true;
    public ItemCategory category;

    [Header("UI")]
    public string PickupMessage;

    [TextArea] 
    public string Description;

}