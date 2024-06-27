using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Craft;
using _Project.Scripts.Interactable.Collectable;
using _Project.Scripts.Interactable.Craft;
using _Project.Scripts.SkillManagement;
using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.UI;
using _Project.Scripts.UI.Controllers;
using Codice.Client.BaseCommands.Import;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class DetectionController : IEnemyDetector, ICraftDetectorReciver, ICollectableDetectorReciver, IStartable
    {
        [Inject] private PlayerMovementController _playerMovementController;
        // [Inject] private BotController _botController;

        [Inject] private BarController _barController;
        // [Inject] private UIMediator _uiMediator;
        //private PlayerInGameUpgradeBarController _playerInGameUpgradeBarController;
        private PlayerSM _playerSM;
        private ITargetable _target;
        public ITargetable Target => _target;
        private ICraftable _craftable;
        public ICraftable Craftable => _craftable;
        private IEnemy _enemy;
        public IEnemy Enemy => _enemy;
        public bool IsEnemyFound { get; set; }
        public void Initialise(PlayerSM playerSM)
        {
            _playerSM = playerSM;
            //  _playerInGameUpgradeBarController = uIMediator.PlayerInGameUpgradeBarController;
        }
        public void Start()
        {
            //_playerInGameUpgradeBarController = _uiMediator.PlayerInGameUpgradeBarController;
            //  _barController = _skillManager.BarController;
        }
        public void OnCraftableDetect(ICraftable crrCraftable)
        {
            _craftable = crrCraftable;
            SetTarget();
            CheckState();
        }
        public void OnEnemyDetected(IEnemy detectedEnemyInfo)
        {
            _enemy = detectedEnemyInfo;
            SetTarget();
            CheckState();
        }
        private void SetTarget()
        {
            _target = _enemy != null ? _enemy : _craftable;
            _playerMovementController.Target = _target;
            _playerMovementController.IsCloseEnemyFound = _target != null;
        }
        private void CheckState()
        {
            if (_enemy != null)
            {
                IsEnemyFound = true;
                _playerSM.ChangeState(_playerSM.AttackState);
            }
            else if (_craftable != null)
            {
                IsEnemyFound = false;
                _playerSM.ChangeState(_playerSM.CraftState);
            }
            else
            {
                IsEnemyFound = false;
                _playerSM.ChangeState(_playerSM.IdleState);
            }
        }

        public void OnCollectableDetected(CollectableType collectableType, Transform collectableTransform)
        {
            StaticHelper.Instance.StartCoroutine(StaticHelper.Instance.MoveToPositionWithFollow(_playerSM.transform, collectableTransform, OnCompletedCollecting));
            if (collectableType == CollectableType.XP) _barController.CollectedXp();
        }

        private void OnCompletedCollecting(Transform collectable)
        {
            collectable.gameObject.SetActive(false);
        }


    }


}