using UnityEngine;

public class ExitButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private UIButtonVisual visual;


    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
            visual = GetComponent<UIButtonVisual>();
    }
    protected override void OnClick()
    {
        visual.PlayPressed();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void OnDeselected()
    {        
        visual.SetHighlighted(false);
    }

    public void OnSelected()
    {
        Debug.Log($"ExitButton is selected");
        visual.SetHighlighted(true);

    }

    public void OnSubmit()
    {
        
    }
}
