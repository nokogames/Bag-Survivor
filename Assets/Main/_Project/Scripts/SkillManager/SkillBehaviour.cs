using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;

using _Project.Scripts.UI.Controllers;
using _Project.Scripts.UI.Interfacies;
using _Project.Scripts.UI.Inventory;
using _Project.Scripts.UI.Inventory.Behaviours;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.SkillManagement
{

    public class SkillBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject skillVisualPref;
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private Image skillBackGround;
        [SerializeField] private Image skillIconHolder;
        [SerializeField] private TextMeshProUGUI infoTxt;
        [SerializeField] private TextMeshPro valueTxt;

        private CreatedSkillInfo _createdSkillInfo;
        private SkillBase _currentSkill;
        private UIMediatorEventHandler _uiEventHandler;
        private Button _btn;
        private SkillVisualBehaviour _createdSkillVisualBehaviour;
        private CustomInventoryData _customInventoryData;
        private InventoryManager _inventoryManager;
        private SkillSizeVisualBehaviour _skillsSizeVisualBehaviour;
        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.RemoveAllListeners();
            _skillsSizeVisualBehaviour = GetComponentInChildren<SkillSizeVisualBehaviour>();
            //_btn.onClick.AddListener(OnBtnClicked);
        }
        internal void Initialize(UIMediatorEventHandler eventHandler, CustomInventoryData customInventoryData, InventoryManager inventoryManager)
        {
            _uiEventHandler = eventHandler;
            _customInventoryData = customInventoryData;
            _inventoryManager = inventoryManager;
            _btn.interactable = true;
           
        }

        public void Setup(CreatedSkillInfo createdSkillInfo)
        {
            _btn.interactable = true;
            ClearVisual();
            _createdSkillInfo = createdSkillInfo;
            _currentSkill = _createdSkillInfo.Skill;
            SetupUI();
            CreateVisual();

        }

        private void CreateVisual()
        {
            _createdSkillVisualBehaviour = Instantiate(skillVisualPref, transform.parent).GetComponent<SkillVisualBehaviour>();
            _createdSkillVisualBehaviour.Initialize(_customInventoryData, _currentSkill.skillVisualData, _inventoryManager, this, _createdSkillInfo);
            _createdSkillVisualBehaviour.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
             _skillsSizeVisualBehaviour.Initialize(_currentSkill.skillVisualData.Size);
            // _createdSkillBehaviour.onPlaceInventory += OnPlaceInventory;
        }

        private void OnPlaceInventory()
        {
            // _createdSkillBehaviour.onPlaceInventory -= OnPlaceInventory;
            _createdSkillVisualBehaviour = null;
        }

        private void ClearVisual()
        {
            if (_createdSkillVisualBehaviour != null) Destroy(_createdSkillVisualBehaviour.gameObject);
        }

        private void SetupUI()
        {
            skillBackGround.sprite = _currentSkill.skillCommenUIInfo.GetSprite(_createdSkillInfo.SkillRarity);
            skillIconHolder.sprite = _currentSkill.Icon;
            infoTxt.text = _currentSkill.GetInfoTxt(_createdSkillInfo.SkillRarity);
            nameTxt.text = _currentSkill.Name;
        }

        public void OnBtnClicked() => _uiEventHandler.OnSkillBtnClicked(_createdSkillInfo);

        internal void DisableMoveSkillVisual()
        {
            if (_createdSkillVisualBehaviour != null) Destroy(_createdSkillVisualBehaviour);
            _btn.interactable = false;
        }

        public void SkillVisualPlaced()
        {
            _createdSkillVisualBehaviour = null;
            _uiEventHandler.OnSkillPlacedInventory();
        }
        public void SkillMovedInInventory()
        {

        }
        public void Kill()
        {

            _createdSkillVisualBehaviour = null;
            _uiEventHandler.OnKillSkill();
        }

    }

}