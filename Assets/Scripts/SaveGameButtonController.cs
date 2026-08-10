using UnityEngine;

public class SaveGameButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private SaveMenuController saveMenuController;
    [SerializeField] private UIButtonVisual visual;

    private Transform playerTransform;

    protected override void Awake()
    {
        base.Awake();
        if (visual == null)
            visual = GetComponent<UIButtonVisual>();
    }

    public void Start()
    {
        Debug.Log("saving station started");
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure they are tagged 'Player'.");
        }
    }
      

    public void OnSelected()
    {
        visual.SetHighlighted(true);
    }

    public void OnDeselected()
    {
        visual.SetHighlighted(false);
    }

    public void OnSubmit()
    {
       
    }

    protected override void OnClick()
    {
       //SaveSystem.SaveGame(playerTransform);
       saveMenuController.DisableSaveScreen();
       Debug.Log("Game has been saved");
    }
}
