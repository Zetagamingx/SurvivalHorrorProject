using System;
using UnityEngine;

public class PauseSelectionModel: MonoBehaviour
{
    public event Action OnPauseSectionChanged;
    private bool canAccessSaveSection = false;

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

    public void EnterSaveSection()
    {
        canAccessSaveSection = true;
        Debug.Log("[MODEL] EnterSaveSection");
        CurrentSection = "SaveSection";
        OnPauseSectionChanged?.Invoke();
    }

    public void ExitSaveSection()
    {
        canAccessSaveSection = false;
        Debug.Log("[MODEL] ExitSaveSection");
        CurrentSection = "MainSection";
        OnPauseSectionChanged?.Invoke();
    }

    public void ResetToDefault()
    {
        Debug.Log($"[MODEL] ResetToDefault BEFORE: {CurrentSection}");

        canAccessSaveSection = false;
        CurrentSection = "MainSection";

        Debug.Log($"[MODEL] ResetToDefault AFTER: {CurrentSection}");
    }
}

