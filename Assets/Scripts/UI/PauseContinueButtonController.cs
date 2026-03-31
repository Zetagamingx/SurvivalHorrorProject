using UnityEngine;

public class PauseContinueButtonController : BasicClickController, IUISelectable
{
    [SerializeField] UIButtonVisual visual;

    protected override void Awake()
    {
        base.Awake();
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
        InGameMenuController.Instance.CloseMenu();
    }

    protected override void OnClick()
    {
        InGameMenuController.Instance.CloseMenu();
    }
}
