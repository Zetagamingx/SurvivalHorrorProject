using System.Collections.Generic;
using UnityEngine;

public class CombineManager : MonoBehaviour
{
    public static CombineManager Instance;

    [SerializeField] private RecipeDatabase recipeDatabase;

    private List<ItemCombineRecipe> recipes = new List<ItemCombineRecipe>();

    
    [SerializeField] private Inventory inventory;

    public void Awake()
    {
        Instance = this;

        if (recipeDatabase != null)
        {
            recipes = new List<ItemCombineRecipe>(recipeDatabase.allRecipes);
            Debug.Log($"[CombineManager] Loaded {recipes.Count} recipes from RecipeDatabase.");
        }
        else
        {
            Debug.LogWarning("No RecipeDatabase assigned to CombineManager.");
        }
    }
    public bool TryCombine(ItemData itemA, ItemData itemB)
    {
        foreach (var recipe in recipes)
        {
            if (IsMatch(recipe, itemA, itemB))
            {
                //  Check if inventory contains both items first
                if (!inventory.HasItem(itemA.itemName, 1) || !inventory.HasItem(itemB.itemName, 1))
                {
                    Debug.LogWarning(" Not enough items in inventory to combine.");
                    return false;
                }

                inventory.RemoveItem(itemA.itemName, 1);
                inventory.RemoveItem(itemB.itemName, 1);
                inventory.AddItem(recipe.result.itemName, 1);

                Debug.Log($"ready for Combined {itemA.itemName} + {itemB.itemName} thus result in {recipe.result.itemName}");
                return true;
            }
        }

        Debug.Log(" No matching recipe found.");
        return false;
    }

    private bool IsMatch(ItemCombineRecipe recipe, ItemData a, ItemData b)
    {
        return (recipe.inputA == a && recipe.inputB == b) ||
               (recipe.inputA == b && recipe.inputB == a);
    }
}
