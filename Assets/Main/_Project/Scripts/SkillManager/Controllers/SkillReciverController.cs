using System;
using System.Collections.Generic;

using System.Linq;


using _Project.Scripts.UI.Interfacies;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using _Project.Scripts.UI.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Level;
using Cysharp.Threading.Tasks;
using _Project.Scripts.Character.Runtime;
using _Project.Scripts.UI;

namespace _Project.Scripts.SkillManagement.Controllers
{

    public class SkillReciverController : ISkillReciever, IDisposable
    {
        [Inject] private UIMediatorEventHandler _uiMediatorEventHandler;
        [Inject] private SkillUIController _skillUIController;

        [Inject] private SkillCreator _skillCreator;
        // [Inject] private PlayerInGameUpgradeBarController _playerInGameUpgradeBarController;
        [Inject] private BarController _barController;
        [Inject] private PlayerUpgradedData _playerUpgradedData;
        [Inject] private InLevelEvents _inLevelEvents;
        [Inject] private InGameSkillController _ingameSkillController;
        [Inject] private TutorialController _tutorialController;

        // [Inject] private SkillCreator _skillCreator;
        public void CloseBtnClicked()
        {


        }

        public void Start()
        {

            _uiMediatorEventHandler.RemoveReciever(this);
            _uiMediatorEventHandler.AddReciever(this);
            _inLevelEvents.onNextLevel += NextLevel;
            _skillUIController.GoBtn.onClick.AddListener(GoBtnClicked);
            // _skillUIControllerData.GoBtn.onClick.AddListener(GoBtnClicked);

        }

        private void GoBtnClicked()
        {
            _skillUIController.HidePanel();
            _barController.Upgraded();
        }

        private void NextLevel()
        {
            _playerUpgradedData.Reset();
        }

        public void OnSkillBtnClicked(CreatedSkillInfo createdSkillInfo)
        {
            createdSkillInfo.Skill.OnSelectedSkill(_playerUpgradedData, createdSkillInfo.SkillRarity, _ingameSkillController);
            _skillUIController.HidePanel();
            _barController.Upgraded();
            _tutorialController.SkillTaped();


        }


        // public void OnSkillBtnClicked(CreatedSkillInfo createdSkillInfo)
        // {
        //     createdSkillInfo.Skill.OnSelectedSkill(_playerUpgradedData, createdSkillInfo.SkillRarity, _ingameSkillController);
        //     _skillUIController.HidePanel();
        //     //  _playerInGameUpgradeBarController.Upgraded();
        //     _barController.Upgraded();
        // }


        public void RerollBtnClicked()
        {
            _skillCreator.Reroll();
            Debug.Log($"Reroll");
        }

        public void AbleToUpgrade()
        {

        }




        public void Dispose()
        {
            _uiMediatorEventHandler.RemoveReciever(this);
            _inLevelEvents.onNextLevel -= NextLevel;
        }
        // public void Tick()
        // {
        //     if (Input.GetKeyDown(KeyCode.U)) _skillUIController.ShowPanel();
        // }

        public void OnSkillPlacedInventory()
        {
            _skillCreator.DisableMoveSkillVisual();
        }

        public void OnSkillKill()
        {
            _skillCreator.DisableMoveSkillVisual();
        }
    }
}
