using System;
using System.Collections;
using UnityEngine;

public class FarmTile : MonoBehaviour, IA_Click
{
    [SerializeField] private SpriteRenderer plantHandler;
    public Plant currentPlant;
    private int plantIndex = 0;
    private Material defaultMaterial;
    public static event Action<FarmTile> OnPlantAdded;
    public static event Action<FarmTile> OnPlantRemoved;

    void Awake()
    {
        defaultMaterial = plantHandler.material;
    }

    private void Start()
    {
        if (currentPlant)
        {
            plantProcessCoroutine = StartCoroutine(PlantProcessing());
        }
    }

    public bool AddPlant(Plant plant)
    {
        if (currentPlant != null) return false;

        currentPlant = plant;
        OnPlantAdded?.Invoke(this);
        plantIndex = 0;
        plantProcessCoroutine ??= StartCoroutine(PlantProcessing());
        return true;
    }
    Coroutine plantProcessCoroutine;
    public IEnumerator PlantProcessing()
    {
        while (currentPlant != null && plantIndex < currentPlant.plantGrows.Count)
        {
            plantHandler.sprite = currentPlant.plantGrows[plantIndex].plantSprite;
            float growTime = UnityEngine.Random.Range(currentPlant.plantGrows[plantIndex].minGrowTime, currentPlant.plantGrows[plantIndex].maxGrowTime);
            yield return new WaitForSeconds(growTime);
            plantIndex++;
        }

        Debug.Log("Bitki olgunlaştı");
        if (currentPlant != null) plantHandler.material = currentPlant.itemShineMaterial;
        plantProcessCoroutine = null;

    }
    public void Click()
    {
        Debug.Log("Tıklandı");
        
        if(plantProcessCoroutine != null || currentPlant == null) return;

        Debug.Log("Bitki hasat edildi");
        InventoryManager.Instance.AddItem(currentPlant, 1);
        RemovePlant();
    }
    public void RemovePlant()
    {
        if (plantProcessCoroutine != null)
        {
            StopCoroutine(plantProcessCoroutine);
            plantProcessCoroutine = null;
        }
        plantHandler.material = defaultMaterial;
        currentPlant = null;
        OnPlantRemoved?.Invoke(this);
        plantIndex = 0;
        plantHandler.sprite = null;
    }
}
