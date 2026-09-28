using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultPlant", menuName = "Scriptable Objects/Plants")]
public class Plant : ScriptableObject
{
    public List<PlantGrow> plantGrows = new();
    
}
[System.Serializable]
public class PlantGrow
{
    public Sprite plantSprite;
    public float growTime = 1f;
}