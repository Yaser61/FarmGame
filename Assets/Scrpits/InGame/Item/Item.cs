using UnityEngine;

[CreateAssetMenu(fileName = "new_Item", menuName = "Scriptable Objects/Item/DefaultItem")]
public class Item : ScriptableObject
{
    public string itemName = "Item";
    public Sprite itemSprite;
    public bool isStackable = false;
    public int maxStack = 16;
    public Material itemShineMaterial;
}
