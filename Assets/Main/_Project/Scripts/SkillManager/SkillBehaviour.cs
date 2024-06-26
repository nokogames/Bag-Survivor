using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private Image skillBackGround;
        [SerializeField] private Image skillIconHolder;
        [SerializeField] private TextMeshProUGUI infoTxt;
        [SerializeField] private TextMeshPro valueTxt;

        private SkillBase _currentSkill;
        private UIMediatorEventHandler _uiEventHandler;
        private Button _btn;

        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(OnBtnClicked);
        }
        internal void Initialize(UIMediatorEventHandler eventHandler)
        {
            _uiEventHandler = eventHandler;
        }
      
        public void Setup(SkillBase skill)
        {
            _currentSkill = skill;
            SetupUI();
        }

        private void SetupUI()
        {
            skillBackGround.sprite = _currentSkill.skillCommenUIInfo.GetSprite(_currentSkill.CurrentRarity);
            skillIconHolder.sprite = _currentSkill.Icon;

        }

        public void OnBtnClicked() => _uiEventHandler.OnSkillBtnClicked(_currentSkill);


    }

}