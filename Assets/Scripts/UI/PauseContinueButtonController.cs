using UnityEngine;

public class PauseContinueButtonController : BasicClickController, IUISelectable
{
    [SerializeField] UIButtonVisual visual;
    [SerializeField] private PauseMenuController pauseMenuController;
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
        Debug.Log($"PauseContinue is selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        
    }

    protected override void OnClick()
    {
        pauseMenuController.ClosePauseMenu();
    }
}
