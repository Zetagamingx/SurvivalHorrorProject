using System;
using UnityEngine;

public class StartButtonController : BasicClickController, IUISelectable
{

    [SerializeField] private UIButtonVisual visual;

    public static event Action OnBeginGame;

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
        AudioManager.Instance.PlaySfx("crackedoor");
        OnBeginGame?.Invoke();

        Debug.Log("Start button pressed.");
    }
}
