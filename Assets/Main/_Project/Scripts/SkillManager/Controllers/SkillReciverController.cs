using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

using _Project.Scripts.UI.Interfacies;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using _Project.Scripts.UI.Controllers;
using _Project.Scripts.SkillManagement.SO.Skills;
using _Project.Scripts.Character.Datas.SO;

namespace _Project.Scripts.SkillManagement.Controllers
{

    public class SkillReciverController : ISkillReciever, IStartable, IDisposable, ITickable
    {
        [Inject] private UIMediatorEventHandler _uiMediatorEventHandler;
        [Inject] private SkillUIController _skillUIController;
        [Inject] private SkillCreator _skillCreator;
        // [Inject] private PlayerInGameUpgradeBarController _playerInGameUpgradeBarController;
        [Inject] private BarController _barController;
        [Inject] private PlayerUpgradedData _playerUpgradedData;
        // [Inject] private SkillCreator _skillCreator;
        public void CloseBtnClicked()
        {

            Debug.Log($"Manger----CloseBtn");
        }


        public void Start()
        {
            // _playerInGameUpgradeBarController.SkillReciever = this;
            _uiMediatorEventHandler.AddReciever(this);
        }

        public void OnSkillBtnClicked(CreatedSkillInfo createdSkillInfo)
        {
            createdSkillInfo.Skill.OnSelectedSkill(_playerUpgradedData, createdSkillInfo.SkillRarity);
            _skillUIController.HidePanel();
            //  _playerInGameUpgradeBarController.Upgraded();
            _barController.Upgraded();
        }



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
        }
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.U)) _skillUIController.ShowPanel();
        }
    }
}
