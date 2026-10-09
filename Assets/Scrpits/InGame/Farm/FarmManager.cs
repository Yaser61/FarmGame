using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public List<FarmTile> plantedTiles = new();
    public static FarmManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        FarmTile.OnPlantAdded += AddPlantedTiles;
        FarmTile.OnPlantRemoved += RemovePlantedTiles;
    }

    void ODisable()
    {
        FarmTile.OnPlantAdded -= AddPlantedTiles;
        FarmTile.OnPlantRemoved -= RemovePlantedTiles;
    }

    public void AddPlantedTiles(FarmTile tile)
    {
        plantedTiles.Add(tile);
    }
    public void RemovePlantedTiles(FarmTile tile)
    {
        plantedTiles.Remove(tile);
    }
}
