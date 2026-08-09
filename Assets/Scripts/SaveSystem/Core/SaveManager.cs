using UnityEngine;

public class SaveManager : MonoBehaviour
{
    
    [SerializeField] PlayerInventory playerInventory;

    public void SaveGame()
    {
        SaveSystem.SaveGame(playerInventory);
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame(playerInventory);
    }
}
