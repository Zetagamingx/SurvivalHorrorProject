using UnityEngine;
using UnityEngine.SceneManagement;
public class ItemDialogueButtonController : BasicClickController, IUISelectable
{

    [SerializeField] private UIButtonVisual visual;
    [SerializeField] private GameObject tomb;
    [SerializeField] private GameObject gameEndContainer;
    [SerializeField] private GameObject itemDialogueContainer;

    private BoxCollider boxCollider;

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
        AudioManager.Instance.PlayMusic("EndScreen");
        gameEndContainer.SetActive(true);
        itemDialogueContainer.SetActive(false);
    }
}
