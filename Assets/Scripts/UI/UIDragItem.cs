using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class UIDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private UIInventorySlot inventorySlot;

    public int SlotIndex { get; set; }

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public RectTransform RectTransform => rectTransform;

    private Transform originalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (playerInventory == null)
            playerInventory = FindFirstObjectByType<PlayerInventory>();

        if (inventorySlot == null)
            inventorySlot = GetComponentInParent<UIInventorySlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"Dragging slot {SlotIndex}");

        originalParent = transform.parent;
        //transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        //transform.SetParent(originalParent, false);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (!playerInventory.IsCombineMode)
            return;

        playerInventory.SelectSlotForCombination(inventorySlot.SlotIndex);
    }
}