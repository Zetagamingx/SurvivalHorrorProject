using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(OnClickButtonHelper))]
public abstract class BasicClickController : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    protected OnClickButtonHelper buttonHelper;

    protected virtual void Awake()
    {
        buttonHelper = GetComponent<OnClickButtonHelper>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer reached button");
        if (!buttonHelper.Interactable)
            return;

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                OnClick();
                break;

            case PointerEventData.InputButton.Right:
                OnRightClick();
                break;
        }
    }

    protected virtual void OnRightClick()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!buttonHelper.Interactable)
            return;

        if (this is IUISelectable selectable)
            selectable.OnSelected();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!buttonHelper.Interactable)
            return;

        if (this is IUISelectable selectable)
            selectable.OnDeselected();
    }

    protected abstract void OnClick();
}