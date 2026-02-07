using UnityEngine;

public class IntroSceneToHospitalSectionScene : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => null;

    public void Interact()
    {
        SceneManagerController.Instance.LoadScene(GameScene.HospitalScene);
    }

    
}
