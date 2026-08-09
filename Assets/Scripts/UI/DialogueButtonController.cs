using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private string SectionToActivate;
    [SerializeField] private UIButtonVisual visual;

    [HideInInspector] public NPCInteraction npcInteraction;

    [SerializeField] private TextMeshProUGUI questionTextMeshPro;
    [SerializeField] private TextMeshProUGUI answerOneTextMeshPro;
    [SerializeField] private TextMeshProUGUI answerTwoTextMeshPro;

    [SerializeField] private List<string> question = new();
    [SerializeField] private List<string> answerOne = new();
    [SerializeField] private List<string> answerTwo = new();

    [SerializeField] private int maxQuestions = 2;

    private int questionNumber = 0;     // Global question index
    private int questionsAsked = 0;     // Questions asked by the current NPC

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

   
    public void StartConversation()
    {
        questionsAsked = 0;
    }

    protected override void OnClick()
    {
        AudioManager.Instance.PlaySfx("Confirm");

        if (questionsAsked < maxQuestions &&
            questionNumber < question.Count)
        {
            conversationSelectionModel.ShowSection(SectionToActivate);

            questionTextMeshPro.SetText(question[questionNumber]);
            answerOneTextMeshPro.SetText(answerOne[questionNumber]);
            answerTwoTextMeshPro.SetText(answerTwo[questionNumber]);

            questionNumber++;
            questionsAsked++;
        }
        else
        {
            npcInteraction.EndConversation();
        }
    }

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSelected()
    {
        AudioManager.Instance.PlaySfx("Selected");
        Debug.Log("DialogueSelectionButton is selected");
        visual.SetHighlighted(true);
    }

    public void OnSubmit()
    {
    }
}