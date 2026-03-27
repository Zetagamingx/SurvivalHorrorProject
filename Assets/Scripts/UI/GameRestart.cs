using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameRestart
{
    public static void FullRestart()
    {
        SceneManagerController.Instance.RestartGame();
    }
}