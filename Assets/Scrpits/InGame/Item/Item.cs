using UnityEngine;

[CreateAssetMenu(fileName = "new_Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public Sprite itemSprite;
    public bool isStackable = false;
    public int maxStack = 16;
}
