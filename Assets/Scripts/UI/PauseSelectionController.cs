using UnityEngine;

public class PauseSelectionController : BasicClickController, IUISelectable
{
    [SerializeField] public string sectionToActivate;
    [SerializeField] private UIButtonVisual visual;

    private PauseSelectionModel pauseSelectionModel;
    private PauseSelectionViewModel pauseSelectionViewModel;

    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
            visual = GetComponent<UIButtonVisual>();
        pauseSelectionModel = GetComponentInParent<PauseSelectionModel>();
        pauseSelectionViewModel = GetComponentInParent<PauseSelectionViewModel>();
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
        
    }

    protected override void OnClick()
    {
        visual.SetHighlighted(false);
        pauseSelectionModel.ShowSection(sectionToActivate);
    }
}
