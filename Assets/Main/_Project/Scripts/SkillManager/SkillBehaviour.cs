using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI.Controllers;
using _Project.Scripts.UI.Interfacies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.SkillManagement
{

    public class SkillBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameTxt;
        [SerializeField] private Image skillBackGround;
        [SerializeField] private Image skillIconHolder;
        [SerializeField] private TextMeshProUGUI infoTxt;
        [SerializeField] private TextMeshPro valueTxt;

        private CreatedSkillInfo _createdSkillInfo;
        private SkillBase _currentSkill;
        private UIMediatorEventHandler _uiEventHandler;
        private Button _btn;

        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.RemoveAllListeners();
            _btn.onClick.AddListener(OnBtnClicked);
        }
        internal void Initialize(UIMediatorEventHandler eventHandler)
        {
            _uiEventHandler = eventHandler;
        }

        public void Setup(CreatedSkillInfo createdSkillInfo)
        {
            _createdSkillInfo = createdSkillInfo;
            _currentSkill = _createdSkillInfo.Skill;
            SetupUI();
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