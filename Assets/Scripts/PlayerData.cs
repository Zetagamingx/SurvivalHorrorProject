using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float playerXposition;
    public float playerYposition;
    public float playerZposition;

    public PlayerData(Vector3 position)
    {
        playerXposition = position.x;
        playerYposition = position.y;
        playerZposition = position.z;
    }
    public Vector3 ToVector3()
    {
        return new Vector3(playerXposition, playerYposition, playerZposition);
    }
}

