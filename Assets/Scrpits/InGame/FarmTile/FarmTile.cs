using System.Collections;
using UnityEngine;

public class FarmTile : MonoBehaviour, IA_Click
{
    [SerializeField] private SpriteRenderer plantHandler;
    [SerializeField] private Plant currentPlant;
    private int plantIndex = 0;

    private void Start()
    {
        if (currentPlant)
        {
            plantProcessCoroutine = StartCoroutine(PlantProcessing());
        }
    }

    public void AddPlant(Plant plant)
    {
        if (currentPlant != null) return;

        currentPlant = plant;
        plantIndex = 0;
        plantProcessCoroutine ??= StartCoroutine(PlantProcessing());
    }
    Coroutine plantProcessCoroutine;
    public IEnumerator PlantProcessing()
    {
        while (plantIndex < currentPlant.plantGrows.Count)
        {
            plantHandler.sprite = currentPlant.plantGrows[plantIndex].plantSprite;
            yield return new WaitForSeconds(currentPlant.plantGrows[plantIndex].growTime);
            plantIndex++;
        }

        Debug.Log("Bitki olgunlaştı");
        plantProcessCoroutine = null;

    }
    public void Click()
    {
        Debug.Log("Tıklandı");
        
        if(plantProcessCoroutine != null || currentPlant == null) return;

        Debug.Log("Bitki hasat edildi");

        InventoryManager.Instance.AddItem(currentPlant, 1);
        currentPlant = null;
        plantIndex = 0;
        plantHandler.sprite = null;
    }
}
