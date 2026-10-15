using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    public MarketState currentState = MarketState.Buy;
    [SerializeField] private GameObject buyContent;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private List<Item> buyItems = new();

    [SerializeField] private GameObject sellContent;
    [SerializeField] private GameObject sellPanel;
    [SerializeField] private List<Item> sellItems = new();
    [SerializeField] private MarketSlot marketSlotPrefab;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private TextMeshProUGUI goldText;
    public int playerGold = 0;
    public static MarketManager Instance;
    private void Awake() {
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
        FillSlot(buyItems, buyContent);
        FillSlot(sellItems, sellContent);
        buyButton.onClick.AddListener(() =>
        {
            currentState = MarketState.Buy;
            buyPanel.SetActive(true);
            sellPanel.SetActive(false);
        }
        );
        sellButton.onClick.AddListener(() =>
        {
            currentState = MarketState.Sell;
            buyPanel.SetActive(false);
            sellPanel.SetActive(true);
        }
        );
    }

    public void MarketRequest(Item item, int amount)
    {
        switch (currentState)
        {
            case MarketState.Buy:
                if (playerGold >= item.itemPrice)
                {
                    playerGold -= item.itemPrice;
                }
                else Debug.Log("Paran yetmiyor.");
                InventoryManager.Instance.AddItem(item, amount);
                goldText.text = playerGold.ToString();
                break;
            case MarketState.Sell:
                
                bool condition = InventoryManager.Instance.RemoveItem(item, amount);
                if (condition)
                {
                    playerGold += item.itemPrice;
                    goldText.text = playerGold.ToString();
                }
                else Debug.Log("item sende yok satılamadı.");
                break;
        }
    }

    public void FillSlot(List<Item> itemList, GameObject parentObject)
    {
        foreach (Item item in itemList)
        {
            MarketSlot slot = Instantiate(marketSlotPrefab, parentObject.transform.position, Quaternion.identity);
            slot.transform.SetParent(parentObject.transform);
            slot.Initialize(item);
        }
    }
}

[System.Serializable]
public enum MarketState
{
    Buy,
    Sell
}