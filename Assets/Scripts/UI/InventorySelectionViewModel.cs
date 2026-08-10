using UnityEngine;

public class InventorySelectionViewModel : MonoBehaviour
{
    [SerializeField] private SimpleStateMachine stateMachine;

    private InventorySelectionModel inventorySelectionModel;

    private void Awake()
    {
        inventorySelectionModel = GetComponent<InventorySelectionModel>();
    }

    private void OnEnable()
    {
        inventorySelectionModel.OnSectionChanged += OnSectionChanged;
        OnSectionChanged();
    }

    private void OnDisable()
    {
        inventorySelectionModel.OnSectionChanged -= OnSectionChanged;
    }

    private void OnSectionChanged()
    {
        stateMachine.SetState(inventorySelectionModel.CurrentSection);
    }
}
