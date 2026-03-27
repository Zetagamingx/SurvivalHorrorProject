using System;
using UnityEngine;

public class TitleSelectionModel : MonoBehaviour
{
    public event Action OnSectionChanged;

    public string CurrentSection { get; private set; } = "MainMenuSection";

    public void ShowSection(string section)
    {
        CurrentSection = section;
        OnSectionChanged?.Invoke();
    }
        
}
