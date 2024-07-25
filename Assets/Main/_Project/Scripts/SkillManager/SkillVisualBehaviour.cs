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

        internal void Initialize(CustomInventoryData customInventoryData, SkillVisualData skillVisualData, InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
            _customInventoryData = customInventoryData;
            onSelectParentTransform = customInventoryData.onSelectParent.GetComponent<RectTransform>();
            ItemHolder = customInventoryData.itemHolder;
            _skillVisualData = skillVisualData;
            size = skillVisualData.Size;
            if (skillVisualData.img != null)
            {

                _img.sprite = skillVisualData.img;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


    }

}