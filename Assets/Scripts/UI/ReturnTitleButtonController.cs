using System;
using UnityEngine;

public class ReturnTitleButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private UIButtonVisual visual;

    public static event Action OnBackToTitle;

    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
            visual = GetComponent<UIButtonVisual>();
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
        GameRestart.FullRestart();
    }

    protected override void OnClick()
    {
        visual.PlayPressed();
        GameRestart.FullRestart();
    }

}
