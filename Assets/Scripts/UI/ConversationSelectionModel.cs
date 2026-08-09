using System;
using UnityEngine;

public class ConversationSelectionModel : MonoBehaviour
{
    public event Action OnSectionChanged;

    public string CurrentSection { get; private set; } = "DialogueSection";

    public void ShowSection(string section)
    {
        CurrentSection = section;
        OnSectionChanged?.Invoke();
    }
        
}
