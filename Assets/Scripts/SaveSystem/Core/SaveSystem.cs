using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string filePath =
        Path.Combine(Application.persistentDataPath, "SaveData.json");

    public static void SaveGame(PlayerInventory playerInventory)
    {
        SaveData data = new SaveData();

        
        data.inventory = playerInventory.GetInventoryData();

        
        data.objects = new List<ObjectStateData>();

        SaveableObject[] saveables = Object.FindObjectsByType<SaveableObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);

        foreach (SaveableObject obj in saveables)
        {
            data.objects.Add(new ObjectStateData
            {
                id = obj.GetID(),
                isActive = obj.GetState(),
                hasComponent = obj.HasComponent(),
                componentEnabled = obj.HasComponent() ? obj.GetComponentState() : false
            });
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Game Saved");
    }

    public static void LoadGame(PlayerInventory playerInventory)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(filePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        
        
        playerInventory.LoadInventoryData(data.inventory);

       
        SaveableObject[] saveables = Object.FindObjectsByType<SaveableObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None);

        foreach (SaveableObject obj in saveables)
        {
            foreach (ObjectStateData saved in data.objects)
            {
                if (obj.GetID() != saved.id)
                    continue;

                obj.ApplyState(saved.isActive);

                if (saved.hasComponent)
                    obj.ApplyComponentState(saved.componentEnabled);

                break;
            }
        }

        Debug.Log("Game Loaded");
    }

    public static bool SaveExists()
    {
        return File.Exists(filePath);
    }

    public static SaveData LoadSaveDataOnly()
    {
        if (!SaveExists())
            return null;

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}