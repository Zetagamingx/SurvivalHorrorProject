using UnityEngine;

public class ExitButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private UIButtonVisual visual;


    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
            visual = GetComponent<UIButtonVisual>();
    }
    protected override void OnClick()
    {
        SceneManagerController.Instance.LoadScene(GameScene.IntroScene);
    }

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSelected()
    {
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        visual.PlayPressed();

        // Call ViewModel / Model logic here
        Debug.Log("Start button pressed.");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
