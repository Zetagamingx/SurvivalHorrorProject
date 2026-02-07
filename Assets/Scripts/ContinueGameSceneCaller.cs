using UnityEngine;

public class ContinueGameSceneCaller : MonoBehaviour
{

    public GameScene targetScene;

    public void LoadTargetScene()
    {
        SceneManagerController.Instance.LoadScene(targetScene);
    }

    // Optional: Trigger automatically on player collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadTargetScene();
        }
    }
}

