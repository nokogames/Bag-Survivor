using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;
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
        InventoryManager inventoryManager, SkillBehaviour skillBehaviour, CreatedSkillInfo createdSkillInfo)
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
            _skillBase = createdSkillInfo.Skill;
            _skillRarity = createdSkillInfo.SkillRarity;
        }

        public override void NotPlaced()
        {

        }
        public override void PlacedToInventory()
        {
            if (_skillBehavior != null) _skillBehavior.SkillVisualPlaced();
        }

        public override void MovedInInventory()
        {
            if (_skillBehavior != null) _skillBehavior.SkillMovedInInventory();
        }
        internal override void Kill()
        {
            if (_skillBehavior != null && !IsPlacedInventory) _skillBehavior.Kill();
            base.Kill();
        }



    }

}