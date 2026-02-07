using UnityEngine;

public class SaveStationUIController : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure they are tagged 'Player'.");
        }
    }

    public void OnSaveButtonClicked()
    {
        if (playerTransform != null)
        {
            SaveSystem.SavePlayerPosition(playerTransform.position);
        }
    }
}
