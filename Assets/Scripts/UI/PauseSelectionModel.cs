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

        CurrentSection = section;
        OnPauseSectionChanged?.Invoke();
    }

    public void EnterSaveSection()
    {
        canAccessSaveSection = true;

        CurrentSection = "SaveSection";
        OnPauseSectionChanged?.Invoke();
    }

    public void ExitSaveSection()
    {
        canAccessSaveSection = false;

        CurrentSection = "MainSection";
        OnPauseSectionChanged?.Invoke();
    }
}

