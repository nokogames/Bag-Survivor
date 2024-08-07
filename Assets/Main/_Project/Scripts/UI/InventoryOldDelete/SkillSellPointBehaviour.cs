using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
namespace _Project.Scripts.UI.Inventory
{

    public class SkillSellPointBehaviour : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private InventoryManager _inventoryManager;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.gameObject.TryGetComponent<SkillVisualBehaviour>(out var skill)) _inventoryManager.IsSelling = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = Vector3.one * 1.1f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        internal void Initialize(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;

        }




    }
}
