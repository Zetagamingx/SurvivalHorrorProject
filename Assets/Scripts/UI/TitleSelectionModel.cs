using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
