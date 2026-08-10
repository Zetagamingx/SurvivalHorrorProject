using UnityEngine;

public class ExitSaveButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private SaveMenuController saveMenuController;
    [SerializeField] private UIButtonVisual visual;

    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
        {
            visual = GetComponent<UIButtonVisual>();
        }
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
        //throw new System.NotImplementedException();
    }

    protected override void OnClick()
    {
        visual.SetHighlighted(false);
        saveMenuController.DisableSaveScreen();
    }
}
