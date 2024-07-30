using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
namespace _Project.Scripts.UI.Inventory
{

    public class SkillSellPointBehaviour : MonoBehaviour, IDropHandler
    {
        private InventoryManager _inventoryManager;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent<SkillVisualBehaviour>(out var skill)) _inventoryManager.IsSelling = true;
        }

        internal void Initialize(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;

        }




    }
}
