using System;
using UnityEngine;

public class PauseSelectionModel: MonoBehaviour
{
    public event Action OnSectionChanged;
    
    public string CurrentSection { get; private set;} = ("MainSection");

    public void ShowSection(string section)
    {
        CurrentSection = section;
        OnSectionChanged?.Invoke();
    }
   
}

