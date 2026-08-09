using System;
using UnityEngine;

public class LoadGameButtonController : BasicClickController, IUISelectable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        AudioManager.Instance.PlaySfx("crackedoor");
        if (GameLoader.Instance == null)
        {
            Debug.LogError("GameLoader.Instance is NULL");
            return;
        }

        GameLoader.Instance.ContinueGame();

        Debug.Log("load game button pressed.");
    }

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSelected()
    {
        Debug.Log($"LoadGame is selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        
    }
}

