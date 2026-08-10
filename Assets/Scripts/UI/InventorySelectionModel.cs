using System;
using UnityEngine;

public class InventorySelectionModel : MonoBehaviour
{
    public event Action OnSectionChanged;

    public string CurrentSection { get; private set; } = "ItemSection";

    public void ShowSection(string section)
    {
        CurrentSection = section;
        OnSectionChanged?.Invoke();
    }    
}
