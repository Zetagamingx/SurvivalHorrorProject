using UnityEngine;

public class PauseSelectionController : BasicClickController, IUISelectable
{
    [SerializeField] public string sectionToActivate;
    [SerializeField] private UIButtonVisual visual;

    private PauseSelectionModel pauseSelectionModel;
    private PauseSelectionViewModel pauseSelectionViewModel;

    private void Awake()
    {
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
        pauseSelectionModel.ShowSection(sectionToActivate);
    }

    protected override void OnClick()
    {
        pauseSelectionModel.ShowSection(sectionToActivate);
    }
}
