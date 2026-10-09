using UnityEngine;

[CreateAssetMenu(fileName = "new_Weapon", menuName = "Scriptable Objects/Item/Weapon")]
public class Weapon : Item
{
    public int damage = 1;
    public override void ItemUse(GameObject clickedObject)
    {
        base.ItemUse(clickedObject);
        if (clickedObject.TryGetComponent(out IA_Hitable hit))
        {
            hit.Hit(null, damage);
        }
    }
}
