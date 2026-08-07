using UnityEngine;

public class TestButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private UIButtonVisual visual;

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSelected()
    {
        Debug.Log("Im being selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        
    }

    protected override void OnClick()
    {
        
    }

    
}
