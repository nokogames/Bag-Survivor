using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Interactable.Collectable;
using _Project.Scripts.SkillManagement.SO.Skills;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class UpgradeDataApplyer : IPlayerUpgradedReciver, IDisposable, IStartable, IFixedTickable,IUpgradeDataApplyer
    {
        //Datas
        [Inject] private PlayerUpgradedData _upgradedData;
        [Inject] private SavedPlayerData _savedPlayerData;

        //Components
        [Inject] private EnemyDetector _enemyDetector;
        [Inject] private CollectableDetector _collectableDetector;
        //Controllers
        [Inject] private HealthController _healthController;
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private BotController _botController;

        // private float _enemyDetectorBaseRadius = 6f;
        // private float _collectableDetectorBaseRadius = 5.8f;
        public UpgradeDataApplyer()
        {
            CustomExtentions.ColorLog($"UpgradeDataApplyer Created  {this}", Color.yellow);
        }
        public void Start()
        {
            CustomExtentions.ColorLog("Start ", Color.yellow);
            _enemyDetector.SetRadius(_savedPlayerData.range);
            _collectableDetector.SetRadius(_savedPlayerData.pickUpRange);

            _healthController.SetBaseHealtStart(_savedPlayerData.healt);
            _healthController.SetHealingAmount(_savedPlayerData.healtRegenAmount);

            _upgradedData.AddReciver(this);
            _savedPlayerData.AddReciver(this);

            SetMovementSpeed();
            SetBotCount();
            SetCoolDown();
        }
        public void OnUpgraded()
        {

            SetEnemyDetector();
            SetCollectableDetector();
            SetHealt();
            SetHealingAmount();
            SetAdditionalHealingAmount();
            SetMovementSpeed();
            SetBotCount();
            SetCoolDown();
        }



        public void AttacktFixedTick()
        {
            _upgradedData.AttacktFixedTick();
        }

        public void FixedTick()
        {
            _upgradedData.AlwaysFixedTick();
        }
        private void SetHealt()
        {
            _healthController.SetBaseHealt(_savedPlayerData.healt + _upgradedData.healt);
        }
        private void SetAdditionalHealingAmount()
        {
            _healthController.AddHealt(_upgradedData.healt);
            _upgradedData.additionalHelat = 0;
        }
        private void SetHealingAmount()
        {
            _healthController.SetHealingAmount(_savedPlayerData.healtRegenAmount + _upgradedData.healtRegenRate);
        }
        private void SetEnemyDetector()
        {
            _enemyDetector.SetRadius(_savedPlayerData.range + _upgradedData.range);
        }
        private void SetCollectableDetector()
        {
            _collectableDetector.SetRadius(_savedPlayerData.pickUpRange + _upgradedData.pickUpRange);
        }
        private void SetMovementSpeed()
        {
            _playerMovementController.SetSpeed(_savedPlayerData.speed + _upgradedData.speed);
        }
        private void SetBotCount()
        {
            _botController.SetBotCount(_savedPlayerData.botCount);
        }
        public void Dispose()
        {
            _upgradedData.RemoveReciver(this);
        }

        public void SetCoolDown()
        {
            _botController.SetCoolDown(_savedPlayerData.botCoolDownTime, _savedPlayerData.botPlayableTime);
        }

        public void ActivatedSkill(SkillBase skill)
        {
            CustomExtentions.ColorLog($"Activated {skill}", Color.green);

        }

        public void DeactivatedSkill(SkillBase skill)
        {
            CustomExtentions.ColorLog($"Activated {skill}", Color.red);

        }


    }
}