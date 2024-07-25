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
        private SkillVisualBehaviour _createdSkillBehaviour;
        private CustomInventoryData _customInventoryData;
        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.RemoveAllListeners();
            //_btn.onClick.AddListener(OnBtnClicked);
        }
        internal void Initialize(UIMediatorEventHandler eventHandler, CustomInventoryData customInventoryData)
        {
            _uiEventHandler = eventHandler;
            _customInventoryData = customInventoryData;
        }

        public void Setup(CreatedSkillInfo createdSkillInfo)
        {
            ClearVisual();
            _createdSkillInfo = createdSkillInfo;
            _currentSkill = _createdSkillInfo.Skill;
            SetupUI();
            CreateVisual();

        }

        private void CreateVisual()
        {
            _createdSkillBehaviour = Instantiate(skillVisualPref, transform.parent).GetComponent<SkillVisualBehaviour>();
            _createdSkillBehaviour.Initialize(_customInventoryData,_currentSkill.skillVisualData);
            _createdSkillBehaviour.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            _createdSkillBehaviour.onPlaceInventory += OnPlaceInventory;
        }

        private void OnPlaceInventory()
        {
            _createdSkillBehaviour.onPlaceInventory -= OnPlaceInventory;
            _createdSkillBehaviour = null;
        }

        private void ClearVisual()
        {
            if (_createdSkillBehaviour != null) Destroy(_createdSkillBehaviour.gameObject);
        }

        private void SetupUI()
        {
            skillBackGround.sprite = _currentSkill.skillCommenUIInfo.GetSprite(_createdSkillInfo.SkillRarity);
            skillIconHolder.sprite = _currentSkill.Icon;
            infoTxt.text = _currentSkill.GetInfoTxt(_createdSkillInfo.SkillRarity);
            nameTxt.text = _currentSkill.Name;
        }

        public void OnBtnClicked() => _uiEventHandler.OnSkillBtnClicked(_createdSkillInfo);


    }

}