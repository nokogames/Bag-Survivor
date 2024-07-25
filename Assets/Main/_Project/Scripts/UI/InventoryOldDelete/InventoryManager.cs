using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.UI.Inventory.Behaviours;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Inventory
{
    public class InventoryManager : IStartable
    {
        [Inject] private CustomInventoryData _data;
        [Inject] private InventoryGridFactory _gridFactory;
        public void Start()
        {
            _gridFactory.CreateGrid(3, 3, this);
        }


        public bool AddItem(Vector2Int position, Vector2Int size)
        {
            if (CanPlaceItem(position, size))
            {
                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        InventorySlot slot = GetSlotAt(position + new Vector2Int(x, y));
                        if (slot != null)
                        {
                            slot.isOccupied = true;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public void RemoveItem(Vector2Int position, Vector2Int size)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    InventorySlot slot = GetSlotAt(position + new Vector2Int(x, y));
                    if (slot != null)
                    {
                        slot.ClearSlot();
                    }
                }
            }
        }



        private bool CanPlaceItem(Vector2Int position, Vector2Int size)
        {
            ResetColor();
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    InventorySlot slot = GetSlotAt(position + new Vector2Int(x, y));
                    if (slot == null || slot.isOccupied)
                    {
                        return false;
                    }
                    slot.SetColor(Color.red);
                }
            }
            return true;
        }

        private void ResetColor()
        {
            foreach (InventorySlot slot in _data.slots)
            {
                slot.SetColor(Color.white);
            }
        }

        private InventorySlot GetSlotAt(Vector2Int position)
        {
            foreach (var slot in _data.slots)
            {
                if (slot.gridPosition == position)
                {
                    return slot;
                }
            }
            return null;
        }

        internal void OnPointerEnter(InventorySlot inventorySlot, Dragable draggableItem)
        {
            ResetColor();
            var placeableSlots = GetPlaceableSlots(inventorySlot, draggableItem);
            ChangeColorByPlaceable(placeableSlots, CanPlaceItem(inventorySlot.gridPosition, draggableItem.size));

        }

        private void ChangeColorByPlaceable(List<InventorySlot> placeableSlots, bool canPlace)
        {
            Color color = Color.white;
            color = canPlace ? Color.green : Color.red;
            placeableSlots.ForEach(x => x.SetColor(color));

        }

        private List<InventorySlot> GetPlaceableSlots(InventorySlot inventorySlot, Dragable draggableItem)
        {

            List<InventorySlot> result = new();

            for (int x = 0; x < draggableItem.size.x; x++)
            {
                for (int y = 0; y < draggableItem.size.y; y++)
                {
                    InventorySlot slot = GetSlotAt(inventorySlot.gridPosition + new Vector2Int(x, y));
                    if (slot != null)
                    {
                        // slot.isOccupied = true;
                        result.Add(slot);
                    }
                }
            }
            return result;
        }
    }


    [Serializable]
    public class CustomInventoryData
    {
        public Transform itemHolder;
        public Transform onSelectParent;
        public GameObject slotPref;
        public GameObject grid;
        public List<InventorySlot> slots;
    }
}