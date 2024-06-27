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


        private float _enemyDetectorBaseRadius = 3f;
        private float _collectableDetectorBaseRadius = 4f;
        public void Start()
        {

            _enemyDetector.SetRadius(_enemyDetectorBaseRadius);
            _collectableDetector.SetRadius(_collectableDetectorBaseRadius);
            _upgradedData.AddReciver(this);
        }
        public void OnUpgraded()
        {

            SetEnemyDetector();
            SetCollectableDetector();
        }

        private void SetEnemyDetector()
        {
            _enemyDetector.SetRadius(_enemyDetectorBaseRadius + _upgradedData.range);
        }
        private void SetCollectableDetector()
        {
            _collectableDetector.SetRadius(_collectableDetectorBaseRadius + _upgradedData.pickUpRange);
        }

        public void Dispose()
        {
            _upgradedData.RemoveReciver(this);
        }
    }
}