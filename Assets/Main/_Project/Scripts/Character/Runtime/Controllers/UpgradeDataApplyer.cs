using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Datas.SO;
using _Project.Scripts.Interactable.Collectable;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class UpgradeDataApplyer : IStartable, IPlayerUpgradedReciver, IDisposable
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
        public void Start()
        {

            _enemyDetector.SetRadius(_savedPlayerData.range);
            _collectableDetector.SetRadius(_savedPlayerData.pickUpRange);

            _healthController.SetBaseHealt(_savedPlayerData.healt);
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
            SetMovementSpeed();
            SetBotCount();
            SetCoolDown();
        }

        private void SetHealt()
        {
            _healthController.SetBaseHealt(_savedPlayerData.healt + _upgradedData.healt);
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
    }
}