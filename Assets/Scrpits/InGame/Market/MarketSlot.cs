using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketSlot : MonoBehaviour
{
    [SerializeField] private Image slotIcon;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button slotButton;
    public Item currentItem;
    private void Start()
    {
        slotButton.onClick.AddListener(() =>
        {
            MarketManager.Instance.MarketRequest(currentItem, 1);
        });
        Initialize(currentItem);
    }
    public void Initialize(Item newItem)
    {
        currentItem = newItem;
        if (currentItem != null)
        {
            slotIcon.enabled = true;
            slotIcon.sprite = currentItem.itemSprite;
            priceText.text = currentItem.itemPrice.ToString();
        }
        else
        {
            transform.gameObject.SetActive(false);
            slotIcon.enabled = false;
            priceText.text = "";
        }
    }
}