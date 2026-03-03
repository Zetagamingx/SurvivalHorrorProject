using UnityEngine;

public class TitleSelectionController : BasicClickController, IUISelectable
{
    [SerializeField] private string SectionToActivate;
    [SerializeField] private UIButtonVisual visual;

    private TitleSelectionModel titleSelectionModel;
    private TitleSelectionViewModel titleSelectionViewModel;
    
    protected override void Awake()
    {
        base.Awake();

        if (visual == null)
            visual = GetComponent<UIButtonVisual>();

        titleSelectionModel = GetComponentInParent<TitleSelectionModel>(true);
        titleSelectionViewModel = GetComponentInParent<TitleSelectionViewModel>(true);
    }
    protected override void OnClick()
    {
        titleSelectionModel.ShowSection(SectionToActivate);
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
        titleSelectionModel.ShowSection(SectionToActivate);
        
        // Call ViewModel / Model logic here
        Debug.Log("Load Game button pressed.");
    }
}
