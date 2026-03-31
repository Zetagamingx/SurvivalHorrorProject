using UnityEngine;

public class ResumeButtonController : BasicClickController, IUISelectable
{
    [SerializeField] PauseSelectionModel pauseSelectionModel;
    [SerializeField] UIButtonVisual visual;
    protected override void Awake()
    {
        base.Awake();

        if (visual == null)
            visual = GetComponent<UIButtonVisual>();

        pauseSelectionModel = GetComponentInParent<PauseSelectionModel>();
        
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
        pauseSelectionModel.ExitSaveSection();
    }

    protected override void OnClick()
    {
        pauseSelectionModel.ExitSaveSection();
    }
}
