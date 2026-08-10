using UnityEngine;

public class ContinueToGameButtonController : BasicClickController, IUISelectable
{
    [SerializeField] UIButtonVisual visual;
    [SerializeField] PauseMenuController pauseMenuController;

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
        Debug.Log($"ContinueToGame is selected");
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
