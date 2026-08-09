using TMPro;
using UnityEngine;

public class AnswerOneButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private string SectionToActivate;
    [SerializeField] private UIButtonVisual visual;
    [SerializeField] private TextMeshProUGUI dialogueTextMeshPro;
    [SerializeField] private string response;
    [SerializeField] public NPCRewardSystem npcRewardSystem;

    private ConversationSelectionModel conversationSelectionModel;
    private ConversationSelectionViewModel conversationSelectionViewModel;

    protected override void Awake()
    {
        base.Awake();

        if (visual == null)
            visual = GetComponent<UIButtonVisual>();

        conversationSelectionModel = GetComponentInParent<ConversationSelectionModel>(true);
        conversationSelectionViewModel = GetComponentInParent<ConversationSelectionViewModel>(true);
    }
    protected override void OnClick()
    {
        AudioManager.Instance.PlaySfx("Correct");
        npcRewardSystem.correctAnswers++;
        conversationSelectionModel.ShowSection(SectionToActivate);
        dialogueTextMeshPro.SetText(response);
        //AudioManager.Instance.PlaySfx("emptybottlebump");
    }

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSelected()
    {
        AudioManager.Instance.PlaySfx("Selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
        /*visual.PlayPressed();
        conversationSelectionModel.ShowSection(SectionToActivate);
        AudioManager.Instance.PlaySfx("emptybottlebump");

        // Call ViewModel / Model logic here
        Debug.Log("Load Game button pressed.");*/
    }
}
