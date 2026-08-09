using UnityEngine;

public class ResumeButtonController : BasicClickController, IUISelectable
{
    //[SerializeField] private GameObject saveSectionContainer;
    [SerializeField] UIButtonVisual visual;
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
        Debug.Log($"ResumeButton is selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        
    }

    protected override void OnClick()
    {
        visual.SetHighlighted(false);
        //saveSectionContainer.SetActive(false);
    }
}
