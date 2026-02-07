using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    public Inventory inventory; // reference to your inventory script
    public GameObject slotPrefab; // assign your InventorySlot prefab here
    public Transform slotParent; // assign InventoryPanel
    public System.Action OnSlotsCreated;

    private List<GameObject> slotInstances = new List<GameObject>();

    public void Awake()
    {
        Instance = this;
    }

    public void OnEnable()
    {
        Inventory.OnInventoryChanged += UpdateUI;
    }

    public void OnDisable()
    {
        Inventory.OnInventoryChanged += UpdateUI;
    }

    void Start()
    {

        if (inventory == null)
            inventory = Inventory.Instance;

        CreateSlots();
        UpdateUI();
    }

    void CreateSlots()
    {
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slotInstances.Add(slot);
        }
        OnSlotsCreated?.Invoke();
    }

    public void UpdateUI()
    {
         for (int i = 0; i < inventory.maxSlots; i++)
    {
        var slot = inventory.slots[i];
        var slotGO = slotInstances[i];

        Image icon = slotGO.transform.Find("ItemIcon").GetComponent<Image>();

        Transform quantityBG = slotGO.transform.Find("QuantityBG");
        Image bgImage = quantityBG.GetComponent<Image>();
        TextMeshProUGUI quantityText = quantityBG.Find("QuantityText").GetComponent<TextMeshProUGUI>();

        if (!slot.IsEmpty)
        {
            icon.sprite = slot.item.icon;
            icon.color = Color.white;

            if (slot.quantity > 1)
            {
                quantityText.text = slot.quantity.ToString();
                quantityText.color = new Color(quantityText.color.r, quantityText.color.g, quantityText.color.b, 1f);
                bgImage.color = Color.black; // or whatever visible color you use
            }
            else
            {
                quantityText.text = "";
                quantityText.color = new Color(quantityText.color.r, quantityText.color.g, quantityText.color.b, 0f);
                bgImage.color = new Color(0, 0, 0, 0);      // invisible
            }
        }
        else
        {
            icon.sprite = null;
            icon.color = new Color(1, 1, 1, 0); // transparent

            quantityText.text = "";
            quantityText.color = new Color(1, 1, 1, 0);
            bgImage.color = new Color(0, 0, 0, 0);
        }
    }
    }
}