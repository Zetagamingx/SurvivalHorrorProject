using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimpleStateMachine))]
public class UIChildStateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleStateMachine machine = (SimpleStateMachine)target;

        if (GUILayout.Button("Refresh Children"))
        {
            machine.RefreshChildren();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("States", EditorStyles.boldLabel);

        // Everything On
        if (GUILayout.Button("Everything On"))
        {
            machine.SetAll(true);
            machine.SetDefaultState("Everything On");
        }

        // Everything Off
        if (GUILayout.Button("Everything Off"))
        {
            machine.SetAll(false);
            machine.SetDefaultState("Everything Off");
        }

        EditorGUILayout.Space();

        foreach (var child in machine.Children)
        {
            if (child == null) continue;

            if (GUILayout.Button(child.name))
            {
                machine.SetState(child.name);
                machine.SetDefaultState(child.name);
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Default State (Manual Override)");
        machine.SetDefaultState(EditorGUILayout.TextField(machine.DefaultState));
    }
}