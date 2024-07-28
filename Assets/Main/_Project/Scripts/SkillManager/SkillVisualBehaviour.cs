using System;
using System.Collections;
using System.Collections.Generic;

using _Project.Scripts.UI.Inventory;
using _Project.Scripts.UI.Inventory.Behaviours;
using UnityEngine;
namespace _Project.Scripts.SkillManagement
{

    public class SkillVisualBehaviour : Dragable
    {
        private CustomInventoryData _customInventoryData;
        private SkillBehaviour _skillBehavior;

        internal void Initialize(CustomInventoryData customInventoryData, SkillVisualData skillVisualData,
        InventoryManager inventoryManager, SkillBehaviour skillBehaviour)
        {
            _inventoryManager = inventoryManager;
            _customInventoryData = customInventoryData;
            onSelectParentTransform = customInventoryData.onSelectParent.GetComponent<RectTransform>();
            ItemHolder = customInventoryData.itemHolder;
            _skillVisualData = skillVisualData;
            size = skillVisualData.Size;
            _skillBehavior = skillBehaviour;
            if (skillVisualData.img != null)
            {
                bgImg.sprite = skillVisualData.img;
                bgImg.SetNativeSize();
                _img.sprite = skillVisualData.img;
                _img.SetNativeSize();
            }
        }

        public override void NotPlaced()
        {

        }
        public override void PlacedToInventory()
        {
            if (_skillBehavior != null) _skillBehavior.SkillVisualPlaced();
        }


    }

}