using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SaveSystemV3
{
    private static string filePath = Application.persistentDataPath + "/saveData1.json";
    //[System.Diagnostics.DebuggerStepThrough]
    public static void SaveGame(Transform playerTransform)
    {
        Debug.Log("BREAK TEST A");   // put breakpoint here

        SaveData data = new SaveData();

        data.objects = new List<ObjectStateData>();

        // Player
        data.player = new PlayerData(playerTransform.position);

        //Scene

        data.scene = SceneManagerController.Instance.GetCurrentGameScene();

        // Objects
        List<SaveableObject> saveables = SaveRegistry.GetAll();
        foreach (var obj in saveables)
        {
            if(obj.HasComponent())
            {
                data.objects.Add(new ObjectStateData { id = obj.GetID(), isActive = obj.GetState(), hasComponent = true, componentEnabled = obj.GetComponentState() });
            }

            else
                data.objects.Add(new ObjectStateData { id = obj.GetID(), isActive = obj.GetState() });

        }
        Debug.Log("BREAK TEST B");   // and here

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public static void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save File Not Found");
            return;
        }

        string json = File.ReadAllText(filePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        //  Restore player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && data.player != null)
        {
            player.transform.position = data.player.ToVector3();
        }

        //  Restore objects
        var saveables = Object.FindObjectsByType<SaveableObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (var obj in saveables)
        {
            foreach (var savedObj in data.objects)
            {
                if (obj.GetID() == savedObj.id)
                {
                    obj.ApplyState(savedObj.isActive);
                    Debug.Log($"LOADED DATA: {savedObj.id}");
                    if (savedObj.hasComponent)
                    {
                        obj.ApplyComponentState(savedObj.componentEnabled);
                    }

                    break;
                }

                else
                    Debug.Log($"SCENE OBJECT: {obj.GetID()}");
            }
        }

        Debug.Log("Game Loaded");
    }

    public static SaveData LoadSaveDataOnly()
    {
        if (!File.Exists(filePath))
            return null;

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
