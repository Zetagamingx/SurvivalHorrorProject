using UnityEngine;

public class TitleSelectionController : BasicClickController
{
    [SerializeField] private string SectionToActivate;

    private TitleSelectionModel titleSelectionModel;
    private TitleSelectionViewModel titleSelectionViewModel;

    protected override void Awake()
    {
        base.Awake();
        titleSelectionModel = GetComponentInParent<TitleSelectionModel>(true);
        titleSelectionViewModel = GetComponentInParent<TitleSelectionViewModel>(true);
    }
    protected override void OnClick()
    {
        titleSelectionModel.ShowSection(SectionToActivate);
    }
}
