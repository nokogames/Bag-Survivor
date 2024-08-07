using System;
using System.Collections.Generic;
using _Project.Scripts.Level;
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
        [Inject] private InLevelEvents _inLvelEvents;

        private List<Dragable> _addedDragable;
        public Transform PlayerTransform { get; set; }
        public void Start()
        {
            _addedDragable = new();
            _gridFactory.CreateGrid(3, 3, this);
            _data.skillSellPointBehaviour.Initialize(this);
            _inLvelEvents.onNextLevel += OnNextLevel;
        }

        private void OnNextLevel()
        {
            _data.slots.ForEach(x => x.Reset());
            _addedDragable.ForEach(x => x.Kill());
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
                            slot.IsOccupied = true;
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

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    InventorySlot slot = GetSlotAt(position + new Vector2Int(x, y));
                    if (slot == null || slot.IsOccupied)
                    {
                        return false;
                    }
                    //   slot.SetColor(Color.red);
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
            // var canPlace = CanPlaceItem(inventorySlot.gridPosition, draggableItem.size);
            // draggableItem.CanPlace = canPlace;
            // if (canPlace) _selectedDragable.startPlaceInventorySlot = inventorySlot;
            ChangeColorByPlaceable(placeableSlots, CanPlaceItem(inventorySlot.gridPosition, draggableItem.size));

        }

        private void ChangeColorByPlaceable(List<InventorySlot> placeableSlots, bool canPlace)
        {
            Color color = Color.white;
            color = canPlace ? Color.green : Color.red;
            color = color.SetAlpha(.9f);
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
                    if (slot != null && !slot.IsOccupied)
                    {
                        // slot.isOccupied = true;
                        result.Add(slot);
                    }
                }
            }
            return result;
        }




        // from items
        private Dragable _selectedDragable;
        internal void OnBeginDrag(Dragable dragable)
        {
            if (dragable.InventorySlot != null)
            {
                RemoveItem(dragable.InventorySlot.gridPosition, dragable.size);
            }
            _selectedDragable = dragable;

            SetSkillStatus(dragable, false);
        }

        private void SetSkillStatus(Dragable dragable, bool v)
        {
            if (v) dragable.Skill.ActiveSkill(PlayerTransform, dragable.SkillRarity);
            else dragable.Skill.DeactivateSkill();
        }

        internal void OnDrag()
        {
            if (_selectedDragable == null) return;
            if (_addedDragable.Contains(_selectedDragable)) _addedDragable.Remove(_selectedDragable);
            InventorySlot closestSlot = GetClosestSlot();
            OnPointerEnter(closestSlot, _selectedDragable);

            if (Input.GetKey(KeyCode.A))
            {
                _selectedDragable.Skill.DeactivateSkill();
                _selectedDragable.Kill();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                OnNextLevel();
            }
        }
        public bool IsSelling { get; set; }
        internal void OnEndDrag()
        {
            if (_selectedDragable == null) return;
            if (IsSelling)
            {
                _selectedDragable.Skill.DeactivateSkill();
                _selectedDragable.Kill();
                IsSelling = false;
                ResetColor();
                return;
            }
            InventorySlot closestSlot = GetClosestSlot();
            ResetColor();
            var result = AddItem(closestSlot.gridPosition, _selectedDragable.size);
            // Assert.IsTrue(!result,"Result is false");
            if (result)
            {
                _selectedDragable.CanPlace = true;
                // if (_selectedDragable.startPlaceInventorySlot != null) RemoveItem(_selectedDragable.startPlaceInventorySlot.gridPosition, _selectedDragable.size);
                _selectedDragable.InventorySlot = closestSlot;
                SetSkillStatus(_selectedDragable, true);
            }
            else if (_selectedDragable.InventorySlot != null)
            {
                AddItem(_selectedDragable.InventorySlot.gridPosition, _selectedDragable.size);
            }
            _addedDragable.Add(_selectedDragable);
            //  var placeableSlots = GetPlaceableSlots(closestSlot, _selectedDragable);
            // draggableItem.CanPlace = canPlace;
            // if (canPlace) _selectedDragable.startPlaceInventorySlot = inventorySlot;
            _selectedDragable = null;
        }
        private InventorySlot GetClosestSlot()
        {
            Transform checkPoint = _selectedDragable.checkPoint;
            InventorySlot result = null;
            float distance = float.MaxValue;
            for (int i = 0; i < _data.slots.Count; i++)
            {
                var crrSlot = _data.slots[i];
                var crrDistance = Vector3.Distance(checkPoint.position, crrSlot.transform.position);
                if (crrDistance < distance)
                {
                    result = crrSlot;
                    distance = crrDistance;
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
        public SkillSellPointBehaviour skillSellPointBehaviour;
    }
}