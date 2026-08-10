using UnityEngine;

public class InventorySelectionController : BasicClickController, IUISelectable
{
    [SerializeField] private InventorySelectionModel inventorySelectionModel;
    [SerializeField] private InventorySelectionViewModel inventorySelectionViewModel;

    [SerializeField] private UIButtonVisual visual;

    [SerializeField] private string SectionToActivate;

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
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnClick()
    {
        inventorySelectionModel.ShowSection(SectionToActivate);
    }
}
