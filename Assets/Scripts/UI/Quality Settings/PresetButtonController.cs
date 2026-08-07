using UnityEngine;

public class PresetButtonController : BasicClickController, IUISelectable
{
    
    [SerializeField] private UIButtonVisual visual;
    [SerializeField] private GraphicsPreset preset;

    

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
        QualityPresetsController.instance.SetPreset(preset);
    }

    protected override void OnClick()
    {
        QualityPresetsController.instance.SetPreset(preset);
    }
}
