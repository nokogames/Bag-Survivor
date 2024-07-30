using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.UI.Interfacies;
using _Project.Scripts.UI.Inventory;
using Cysharp.Threading.Tasks;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.UI.Controllers
{
    public class SkillUIController : IStartable
    {
        [Inject] private SkillUIControllerData _skillUIControllerData;
        [Inject] private InputDataSO _inputDataSO;
        [Inject] private UIMediatorEventHandler _eventHandler;
        [Inject] private CustomInventoryData _customInventoryData;
        [Inject] private InventoryManager _inventoryManager;
      
        public Transform SkillTransformParent => _skillUIControllerData.skillParentTransform;
        public List<SkillBehaviour> SkillBehaviors => _skillUIControllerData.skillBehaviors;
        public Button GoBtn =>_skillUIControllerData.GoBtn;
        public Button RerollBtn => _skillUIControllerData.RerollBtn;
        public void Start()
        {
            //_eventHandler.AddReciever(this);
            _skillUIControllerData.RerollBtn.onClick.AddListener(_eventHandler.RerollBtnClicked);
            _skillUIControllerData.RerollBtn.onClick.AddListener(CoolDownTimer);
            for (int i = 0; i < _skillUIControllerData.skillBehaviors.Count; i++) _skillUIControllerData.skillBehaviors[i].Initialize(_eventHandler, _customInventoryData, _inventoryManager);
            Setup();
        }

        private void Setup()
        {
            HidePanel();
        }
        private int _delay = 10;
        public void CoolDownTimer()
        {
            CoolDownTimerAsync().Forget();
        }
        public async UniTaskVoid CoolDownTimerAsync()
        {
            _skillUIControllerData.RerollBtn.interactable = false;
            await UniTask.Delay(_delay);
            _skillUIControllerData.RerollBtn.interactable = true;
        }
        public void ShowPanel()
        {
            _skillUIControllerData.SkillPanel.SetActive(true);
            _inputDataSO.disableJoystick = true;
            // Time.timeScale = 0;
            Time.timeScale = 0;
            // StaticHelper.Instance.gameStatus = GameStatus.Pause;
        }
        public void HidePanel()
        {
            _skillUIControllerData.SkillPanel.SetActive(false);
            _inputDataSO.disableJoystick = false;
            //  StaticHelper.Instance.gameStatus = GameStatus.Playing;
            Time.timeScale = 1;

        }

       




        // //Test
        // public void CloseBtnClicked()
        // {
        //     HidePanel();
        // }

        // public void OnSkillBtnClicked(SkillBase skill)
        // {
        //     // throw new NotImplementedException();
        // }
    }



    [Serializable]
    public class SkillUIControllerData
    {
        public GameObject SkillPanel;
        public Transform skillParentTransform;
        public List<SkillBehaviour> skillBehaviors;
        public Button RerollBtn;
        public Button GoBtn;
    }
}