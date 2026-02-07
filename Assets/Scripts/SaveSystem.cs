using UnityEngine;
using System.IO;
public class SaveSystem
{
    private static string filePath = Application.persistentDataPath + "/saveData1.json";

    public static void SavePlayerPosition(Vector3 position)
    {
        PlayerData data = new PlayerData(position);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log("position saved to:" + filePath);
    }

    public static Vector3 LoadPlayerPosition()
    {
        if (File.Exists(filePath)) 
        {
            string json = File.ReadAllText(filePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Position Loaded:" + data.playerXposition + data.playerYposition + data.playerZposition);
            return data.ToVector3();
        }
        else
        {
            Debug.LogWarning("Save File Not Found");
            return Vector3.zero;

        }
    }
}
