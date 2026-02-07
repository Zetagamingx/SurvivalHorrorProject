using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    public List<ItemCombineRecipe> allRecipes;
}
