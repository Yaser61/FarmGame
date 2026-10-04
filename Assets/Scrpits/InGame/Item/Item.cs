using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new_Item", menuName = "Scriptable Objects/Item/DefaultItem")]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public Sprite itemSprite;
    public bool isStackable = false;
    public int maxStack = 16;
    public Material itemShineMaterial;
    public static event Action<Item, int> ItemUsedAction;
    public virtual void ItemUse(GameObject clickedObject)
    {
    }
    public virtual void ItemUsed(Item item, int amount)
    {
        ItemUsedAction?.Invoke(item, amount);
    }
}
