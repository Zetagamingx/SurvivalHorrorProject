using UnityEngine;

public class InteractDialogueController : MonoBehaviour
{
    public static InteractDialogueController Instance { get; private set; }

    public GameObject dialogueBox;
    public float graceDelay = 1f;

    private bool canClose = false;
    public bool CanClose => canClose;
    public bool isShowingDialogue { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Show()
    {
        Time.timeScale = 0f;
        dialogueBox.SetActive(true);
        isShowingDialogue = true;
        canClose = false;
        StartCoroutine(EnableCloseAfterDelay(graceDelay));
    }

    public void TryClose()
    {
        if (!isShowingDialogue || !canClose)
            return;

        Hide();
    }

    public void Hide()
    {
        Time.timeScale = 1f;
        dialogueBox.SetActive(false);
        isShowingDialogue = false;
        canClose = false;
    }

    private System.Collections.IEnumerator EnableCloseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        canClose = true;
    }
}
