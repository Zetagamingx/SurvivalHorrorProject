using System;
using UnityEngine;

public class ReturnTitleButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private UIButtonVisual visual;

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
        Debug.Log($"ReturnToTitle is selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        
    }

    protected override void OnClick()
    {
        visual.PlayPressed();
        GameRestart.FullRestart();
    }

}
