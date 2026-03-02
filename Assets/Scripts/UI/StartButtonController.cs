using UnityEngine;

public class StartButtonController : BasicClickController
{    
    
    protected override void Awake()
    {
        base.Awake();        
    }
    protected override void OnClick()
    {
        SceneManagerController.Instance.LoadScene(GameScene.IntroScene);
    }
}
