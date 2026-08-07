using UnityEngine;

public class InGameLoadButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private UIButtonVisual visual;
    [SerializeField] private PauseSelectionModel pauseSelectionModel;

    protected override void Awake()
    {
        base.Awake();

        if (visual == null)
            visual = GetComponent<UIButtonVisual>();

        if (pauseSelectionModel == null)
            pauseSelectionModel = GetComponentInParent<PauseSelectionModel>();
    }

    public void OnSelected()
    {
        Debug.Log("LoadGameResumeButton is selected");
        visual.SetHighlighted(true);
    }

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSubmit()
    {
        visual.PlayPressed();
        AudioManager.Instance.PlaySfx("crackedoor");

        // Leave the save section first so the menu returns to MainSection
        pauseSelectionModel.ExitSaveSection();

        if (GameLoader.Instance == null)
        {
            Debug.LogError("GameLoader.Instance is NULL");
            return;
        }

        GameLoader.Instance.ContinueGame();

        Debug.Log("Load game button pressed.");
    }

    protected override void OnClick()
    {
        OnSubmit();
    }
}
