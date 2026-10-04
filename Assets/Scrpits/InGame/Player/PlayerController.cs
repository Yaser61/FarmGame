using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private SpriteRenderer itemHandler;

    private void OnEnable()
    {
        InventoryManager.SelectSlotAction += SlotSelected;
        InventoryManager.DeSelectSlotAction += DeSlotSelected;
    }
    void OnDisable()
    {
        InventoryManager.SelectSlotAction -= SlotSelected;
        InventoryManager.DeSelectSlotAction -= DeSlotSelected;
    }
    void Update()
    {
        if (InventoryManager.Instance.selectedSlot && InventoryManager.Instance.selectedSlot.currentItem)
        {
            Vector2 cor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            itemHandler.sprite = InventoryManager.Instance.selectedSlot.currentItem.itemSprite;
            itemHandler.transform.position = cor;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 cor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D obj = Physics2D.OverlapCircle(cor, 0.5f, clickLayer);
            if (obj == null)
            {
                InventoryManager.Instance.SelectSlot(null);
                return;
            }

            InventoryManager.Instance.selectedSlot.currentItem?.ItemUse(obj.gameObject);

            if (obj.TryGetComponent(out IA_Click click))
            {
                click.Click();
            }
        }
    }
    void SlotSelected(InventorySlot slot)
    {
        if (slot && slot.currentItem)
        {
            itemHandler.sprite = slot.currentItem.itemSprite;
        }
        else itemHandler.sprite = null;
    }
    void DeSlotSelected(InventorySlot slot)
    {
        itemHandler.sprite = null;
    }
}
