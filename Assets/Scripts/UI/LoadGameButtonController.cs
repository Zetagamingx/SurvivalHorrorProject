using System;
using UnityEngine;

public class LoadGameButtonController : BasicClickController, IUISelectable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private UIButtonVisual visual;
    [SerializeField] private GameLoader loader;

    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
            visual = GetComponent<UIButtonVisual>();
    }
    protected override void OnClick()
    {
        //
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
        loader.ContinueGame();

        Debug.Log("Start button pressed.");
    }
}

