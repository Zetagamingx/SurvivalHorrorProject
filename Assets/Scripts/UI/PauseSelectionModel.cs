using System;
using UnityEngine;

public class PauseSelectionModel: MonoBehaviour
{
    public event Action OnPauseSectionChanged;

    public string CurrentSection { get; private set;} = ("MainSection");

    public void ShowSection(string section)
    {
        CurrentSection = section;
        OnPauseSectionChanged?.Invoke();
    }
}
