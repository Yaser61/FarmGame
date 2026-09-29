using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> inventorySlots = new();
    [SerializeField] private List<Item> testItems = new();
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddItem(testItems[Random.Range(0, testItems.Count)], 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem(testItems[Random.Range(0, testItems.Count)], 1);
        }
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
