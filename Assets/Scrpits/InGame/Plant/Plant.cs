using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_Plant", menuName = "Scriptable Objects/Item/Plants")]
public class Plant : Item
{
    public List<PlantGrow> plantGrows = new();
    
}
[System.Serializable]
public class PlantGrow
{
    public Sprite plantSprite;
    public float minGrowTime = 1f;
    public float maxGrowTime = 2f;
}