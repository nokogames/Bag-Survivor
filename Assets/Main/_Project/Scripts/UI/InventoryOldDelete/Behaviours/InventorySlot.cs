using System;
using _Project.Scripts.UI.Inventory;
using _Project.Scripts.UI.Inventory.Behaviours;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isOccupied;
    public bool IsOccupied { get => _isOccupied; set { _isOccupied = value; SetColorByStatus(); } }
    public Image itemImage;
    public Vector2Int gridPosition; // Slotun griddeki pozisyonu
    private InventoryManager _inventoryManager;
    private void Start()
    {
        IsOccupied = false;
    }
    public void Setup(bool isOccupied, Vector2Int gridPosition, InventoryManager inventoryManager)
    {
        this.IsOccupied = isOccupied;
        this.gridPosition = gridPosition;
        _inventoryManager = inventoryManager;

    }
    // public void SetItem(Sprite sprite, Vector2Int size)
    // {
    //     itemImage.sprite = sprite;
    //     itemImage.enabled = true;
    //     isOccupied = true;
    //     // Diğer gerekli işlemler
    // }

    public void ClearSlot()
    {
        // itemImage.sprite = null;
        // itemImage.enabled = false;
        IsOccupied = false;
        // Diğer gerekli işlemler
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Dragable draggableItem = eventData.pointerDrag.GetComponent<Dragable>();
        // if (draggableItem == null) return;

        // _inventoryManager.AddItem(this, draggableItem);
        // draggableItem.startPlaceInventorySlot = this;



    }

    internal void SetColor(Color red)
    {
        // itemImage.color = red;
    }
    internal void SetColorByStatus()
    {
        itemImage.color = _isOccupied ? Color.red.SetAlpha(.6f) : Color.white;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Dragable draggableItem = eventData.pointerDrag.GetComponent<Dragable>();
        // if (draggableItem == null) return;

        // _inventoryManager.OnPointerEnter(this, draggableItem);
        // //SetColor(Color.red);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // SetColor(Color.white);
    }
}
