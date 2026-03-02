using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SimpleStateMachine : MonoBehaviour
{
    [SerializeField] private string defaultState;

    [SerializeField, HideInInspector]
    private List<GameObject> children = new List<GameObject>();

    public string DefaultState => defaultState;
    public List<GameObject> Children => children;

    private void OnEnable()
    {
        RefreshChildren();

        if (Application.isPlaying)
        {
            SetState(defaultState);
        }
    }

    /// <summary>
    /// Reads only first-generation children and stores them.
    /// </summary>
    public void RefreshChildren()
    {
        children.Clear();

        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    /// <summary>
    /// Public runtime-safe method to switch states.
    /// Returns true if successful.
    /// </summary>
    public bool SetState(string stateName)
    {
        if (string.IsNullOrEmpty(stateName))
        {
            Debug.LogWarning("SetState called with null or empty state.");
            return false;
        }

        if (children.Count == 0)
            RefreshChildren();

        // Special states
        if (stateName == "Everything On")
        {
            SetAll(true);
            return true;
        }

        if (stateName == "Everything Off")
        {
            SetAll(false);
            return true;
        }

        bool found = false;

        foreach (var child in children)
        {
            if (child.name == stateName)
            {
                found = true;
                break;
            }
        }

        if (!found)
        {
            Debug.LogWarning($"State '{stateName}' not found on {gameObject.name}");
            return false;
        }

        ApplyState(stateName);
        return true;
    }

    /// <summary>
    /// Applies state without validation.
    /// </summary>
    private void ApplyState(string stateName)
    {
        foreach (var child in children)
        {
            child.SetActive(child.name == stateName);
        }
    }

    public void SetAll(bool value)
    {
        foreach (var child in children)
        {
            child.SetActive(value);
        }
    }

#if UNITY_EDITOR
    public void SetDefaultState(string stateName)
    {
        defaultState = stateName;
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}