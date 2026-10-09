using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventorySlots = new();
    [SerializeField] private List<Item> testItems = new();

    public InventorySlot selectedSlot;
    public static event Action<InventorySlot> SelectSlotAction;
    public static event Action<InventorySlot> FinishSlotAction;
    public static event Action<InventorySlot> DeSelectSlotAction;
    public static InventoryManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        selectedSlot = inventorySlots[0];
    }
    void OnEnable()
    {
        Item.ItemUsedAction += RemoveFromCurrentSlot;
    }
    void OnDisable()
    {
        Item.ItemUsedAction -= RemoveFromCurrentSlot;
    }
    public void SelectSlot(InventorySlot slot)
    {
        //int slotIndex = FindSlot(slot);
        if (selectedSlot)
        {
            selectedSlot.slotSelectedImage.gameObject.SetActive(false);
            DeSelectSlotAction?.Invoke(selectedSlot);
        }   
        selectedSlot = slot;
        SelectSlotAction?.Invoke(selectedSlot);
    }
    public void RemoveFromCurrentSlot(Item item, int amount)
    {
        if (selectedSlot.currentItem && selectedSlot?.currentItem == item)
        {
            selectedSlot.SetItemCount(-amount);
            if(selectedSlot.currentItem == null) FinishSlotAction?.Invoke(selectedSlot);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItems[UnityEngine.Random.Range(0, testItems.Count)], 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem(testItems[UnityEngine.Random.Range(0, testItems.Count)], 1);
        }
    }
    public int FindSlot(InventorySlot slot)
    {
        int index = 0;
        foreach (InventorySlot item in inventorySlots)
        {
            if (item == slot) return index;
            index++;
        }
        return 0;
    }

    public void AddItem(Item item, int amount)
    {
        bool isStackable = item.isStackable;
        if (isStackable)
        {
            InventorySlot slot = GetStackableSlot(item);
            if (slot != null)
            {
                slot.SetItemCount(amount);
            }
            else
            {
                slot = GetEmptySlot();
                if (slot != null) slot.SetItem(item, amount);
                else Debug.Log("boş slot yok");
            }
        }
        else
        {
            InventorySlot slot = GetEmptySlot();
            if (slot != null) slot.SetItem(item, amount);
            else Debug.Log("Boş slot yok");
        }
    }
    public void RemoveItem(Item item, int amount)
    {
        InventorySlot slot = GetItemSlot(item);
        if (slot != null)
            slot.SetItemCount(-amount);
        else Debug.Log("Bu item sende yok: " + item.itemName);
    }
    public InventorySlot GetStackableSlot(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.currentItem != null && slot.currentItem == item)
            {
                if(!slot.isMaxStacked)
                return slot;
            }
        }
        return null;
    }
    public InventorySlot GetEmptySlot()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.currentItem == null)
            {
                return slot;
            }
        }
        return null;
    }
    public InventorySlot GetItemSlot(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.currentItem != null && slot.currentItem == item)
            {
                return slot;
            }
        }
        return null;
    }
}
