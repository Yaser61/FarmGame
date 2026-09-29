using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image slotItemImage;
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] public Item currentItem;
    public int itemCount;
    public bool isMaxStacked;

    private void Start()
    {
        if(currentItem)
        {
            slotItemImage.sprite = currentItem.itemSprite;
            itemCountText.text = itemCount.ToString();
        }
        else
        {
            slotItemImage.sprite = null;
            itemCountText.text = "";
        }
    }

    public void SetItem(Item newItem, int newCount)
    {
        currentItem = newItem;

        Debug.Log("İtem slota geldi ve adı: " + currentItem.itemName);

        itemCount = newCount;
        slotItemImage.sprite = currentItem.itemSprite;
        itemCountText.text = itemCount.ToString();
    }
    public void SetItemCount(int newCount)
    {
        Debug.Log("item değiştirme isteği: " + newCount);
        itemCount += newCount;
        itemCountText.text = itemCount.ToString();
        if (itemCount == currentItem.maxStack) isMaxStacked = true;
        else isMaxStacked = false;

        Debug.Log("İtem sayısı değişti: " + itemCount);

        if (itemCount <= 0) DeleteItem();
    }
    public void DeleteItem()
    {
        itemCount = 0;
        isMaxStacked = false;
        itemCountText.text = "";
        slotItemImage.sprite = null;
        currentItem = null;
        Debug.Log("İtem silindi");
    }
}
