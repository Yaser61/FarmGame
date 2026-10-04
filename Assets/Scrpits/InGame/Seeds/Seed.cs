using UnityEngine;

[CreateAssetMenu(fileName = "new_Seed", menuName = "Scriptable Objects/Item/Seed")]
public class Seed : Item
{
    public Plant plant;
    public override void ItemUse(GameObject clickedObject)
    {
        base.ItemUse(clickedObject);
        if (clickedObject.TryGetComponent(out FarmTile tile))
        {
            bool isAdded = tile.AddPlant(plant);
            if (isAdded) ItemUsed(this, 1);
        }
    }
}