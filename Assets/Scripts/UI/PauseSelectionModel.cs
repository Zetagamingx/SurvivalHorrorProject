using System;
using UnityEngine;

public class PauseSelectionModel: MonoBehaviour
{
    public event Action OnPauseSectionChanged;
    

    public string CurrentSection { get; private set;} = ("MainSection");

    public void ShowSection(string section)
    {
        // Block SaveSection from normal UI navigation
        if (section == "SaveSection")
        {
            Debug.LogWarning("Use EnterSaveSection instead.");
            return;
        }
        Debug.Log($"[MODEL] ShowSection -> {section}");
        CurrentSection = section;
        OnPauseSectionChanged?.Invoke();
    }
   
}

