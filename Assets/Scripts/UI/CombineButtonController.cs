using UnityEngine;
using UnityEngine.UI;

public class CombineButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private string SectionToActivate;
    [SerializeField] private UIButtonVisual visual;
    [SerializeField] private PlayerInventory playerInventory;
    

    private int selectedSlot;
    private Image slotButtonImage;



    protected override void Awake()
    {
        base.Awake();

        if (visual == null)
            visual = GetComponent<UIButtonVisual>();

        

    }
    protected override void OnClick()
    {
        playerInventory.BeginCombineMode();
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
        // visual.PlayPressed();
        //AudioManager.Instance.PlaySfx("emptybottlebump");

        // Call ViewModel / Model logic here
        Debug.Log("Load Game button pressed.");
    }
       
}
