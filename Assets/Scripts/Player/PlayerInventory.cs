using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private RecipeDatabase recipeDatabase;

    [SerializeField] private ItemDatabase itemDatabase;

    [SerializeField] private int maxSlots = 30;

    [SerializeField] private List<InventorySlot> inventory = new();

   

    private int firstSelectedSlot = -1;
    private int secondSelectedSlot = -1;

    private bool combineMode;

    public bool IsCombineMode => combineMode;
    public bool IsCombining => firstSelectedSlot != -1;

    public List<InventorySlot> Inventory => inventory;




    

    private void Awake()
    {
        inventory.Clear();

        for (int i = 0; i < maxSlots; i++)
        {
            inventory.Add(new InventorySlot(null, 0));
        }
    }

    

   

    public void SwapSlots(int fromIndex, int toIndex)
    {
        InventorySlot temp = Inventory[fromIndex];

        Inventory[fromIndex] = Inventory[toIndex];

        Inventory[toIndex] = temp;
    }

    public bool AddItem(ItemData item, int amount)
    {
        //Debug.Log("Tried to add Item");
        foreach (InventorySlot slot in inventory)
        {
            if (!slot.IsEmpty &&
                slot.item == item &&
                item.IsStackable)
            {
                slot.quantity += amount;
                //PrintInventory();
                return true;
            }
        }

        
        foreach (InventorySlot slot in inventory)
        {
            if (slot.IsEmpty)
            {
                slot.item = item;
                slot.quantity = amount;
                //PrintInventory();
                return true;
            }
        }

        Debug.Log("Inventory Full");
        return false;
    }

    public void RemoveItem(ItemData item, int amount)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (slot.item == item)
            {
                slot.quantity -= amount;

                if (slot.quantity <= 0)
                    slot.Clear();

                FindFirstObjectByType<InventoryUIController>().RefreshInventory();

                return;
            }
        }
    }

    public bool HasItem(ItemData item, int amount)
    {
        foreach (InventorySlot slot in inventory)
        {
            if (!slot.IsEmpty &&
                slot.item == item &&
                slot.quantity >= amount)
            {
                return true;
            }
        }

        return false;
    }

    public int ItemInventorySlot(ItemData item)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (!Inventory[i].IsEmpty && Inventory[i].item == item)
            {
                return i;
            }
        }

        return -1;
    }

    public void BeginCombineMode()
    {
        combineMode = true;
        firstSelectedSlot = -1;
        secondSelectedSlot = -1;
    }

    public void EndCombineMode()
    {
        combineMode = false;
        firstSelectedSlot = -1;
        secondSelectedSlot = -1;
    }

    public void BeginCombineMode(int slotIndex)
    {
        firstSelectedSlot = slotIndex;
    }

    public void SelectSlotForCombination(int slotIndex)
    {
        if (!combineMode)
            return;

        if (firstSelectedSlot == -1)
        {
            firstSelectedSlot = slotIndex;
            Debug.Log($"First slot: {slotIndex}");
            return;
        }

        secondSelectedSlot = slotIndex;

        TryCombineItems(firstSelectedSlot, secondSelectedSlot);

        EndCombineMode();
    }
    public bool TryCombineItems(int firstSlot, int secondSlot)
    {
        ItemData itemA = Inventory[firstSlot].item;
        ItemData itemB = Inventory[secondSlot].item;

        foreach (ItemCombineRecipe recipe in recipeDatabase.allRecipes)
        {
            if ((recipe.inputA == itemA && recipe.inputB == itemB) 
                
                ||    
                
                (recipe.inputA == itemB && recipe.inputB == itemA))

            {
                Inventory[firstSlot] = new InventorySlot(recipe.result, 1);

                Inventory[secondSlot].Clear();

                PrintInventory();

                FindFirstObjectByType<InventoryUIController>().RefreshInventory();

                return true;
            }
        }
        return false;
    }

    public void LoadInventoryData(InventoryData data)
    {
        // Clear current inventory
        foreach (InventorySlot slot in inventory)
        {
            slot.Clear();
        }

        // Restore saved items
        foreach (InventorySlotData slotData in data.slots)
        {
            ItemData item = itemDatabase.GetItemByID(slotData.itemID);

            if (item != null)
            {
                inventory[slotData.slotIndex].item = item;
                inventory[slotData.slotIndex].quantity = slotData.quantity;
            }
        }

        FindFirstObjectByType<InventoryUIController>().RefreshInventory();

        PrintInventory();
    }

    public InventoryData GetInventoryData()
    {
        InventoryData data = new InventoryData();

        for (int i = 0; i < inventory.Count; i++)
        {
            InventorySlot slot = inventory[i];

            if (slot.IsEmpty)
                continue;

            data.slots.Add(new InventorySlotData
            {
                slotIndex = i,
                itemID = slot.item.ID,
                quantity = slot.quantity
            });
        }

        return data;
    }
    public void PrintInventory()
    {
        Debug.Log("===== INVENTORY =====");

        for (int i = 0; i < inventory.Count; i++)
        {
            InventorySlot slot = inventory[i];

            if (slot.IsEmpty)
                Debug.Log($"Slot {i}: Empty");
            else
                Debug.Log($"Slot {i}: {slot.item.ItemName} x{slot.quantity}");
        }
    }
}