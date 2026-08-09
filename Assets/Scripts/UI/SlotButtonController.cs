using UnityEngine;
using UnityEngine.UI;

public class SlotButtonController : BasicClickController, IUISelectable
{
    [SerializeField] private string SectionToActivate;
    [SerializeField] private UIButtonVisual visual;

    [SerializeField] GameObject combineButton;

    [SerializeField] private UIInventorySlot inventorySlot;
    [SerializeField] private CombineButtonController combineButtonController;
    [SerializeField] private PlayerInventory playerInventory;


    private Image buttonImage;

    protected override void Awake()
    {
        base.Awake();

        if (visual == null)
            visual = GetComponent<UIButtonVisual>();

        buttonImage = GetComponent<Image>();
        
    }
    protected override void OnClick()
    {
        if (playerInventory.IsCombining)
        {
            //playerInventory.SelectSlotForCombination(inventorySlot.SlotIndex);
        }
    }

    
    protected override void OnRightClick()
    {
        //combineButtonController.SetSelectedSlot(inventorySlot.SlotIndex);
        combineButton.SetActive(true);
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
