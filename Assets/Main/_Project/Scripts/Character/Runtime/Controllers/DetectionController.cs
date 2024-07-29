
using System;
using System.Collections;
using _Project.Scripts.Character.Craft;
using _Project.Scripts.Interactable.Collectable;
using _Project.Scripts.Interactable.Craft;

using _Project.Scripts.SkillManagement.Controllers;
using _Project.Scripts.UI.Controllers;
using Pack.GameData;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class DetectionController : IEnemyDetector, ICraftDetectorReciver, ICollectableDetectorReciver, IStartable
    {
        [Inject] private EnemyDetector _enemyDetector;
        [Inject] private CraftDetector _craftDetector;
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private InGamePanelController _inGamePanelController;
        // [Inject] private BotController _botController;

        [Inject] private BarController _barController;
        [Inject] private GameData _gameData;
        private bool _eneble = true;
        public bool Enable { get => _eneble; set => SetActivity(value); }



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
            // _enemyDetector.Initialise(this);
            // _craftDetector.Initialise(this);
            //_playerInGameUpgradeBarController = _uiMediator.PlayerInGameUpgradeBarController;
            //  _barController = _skillManager.BarController;
        }
        public void OnCraftableDetect(ICraftable crrCraftable)
        {
            if (!_eneble) return;
            _craftable = crrCraftable;
            SetTarget();
            CheckState();
        }
        public void OnEnemyDetected(IEnemy detectedEnemyInfo)
        {
            if (!_eneble) return;
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
            if (!_eneble) return;
            if(_barController==null) Debug.LogError("_barController is null");
            //  var coroutine = StaticHelper.Instance.MoveToPositionWithFollow(_playerSM.Transform, collectableTransform, collectableType, OnCompletedCollecting);\
            if (collectableType == CollectableType.XP) _barController.CollectedXp();
            else if (collectableType == CollectableType.GEM) CollectedGem();

            var coroutine = collectableTransform.CustomDoJump(_playerSM.Transform, collectableType, 3f, .3f, OnCompletedCollecting);
            StaticHelper.Instance.StartHelperCoroutine(coroutine);

        }

        private void CollectedGem()
        {
            _gameData.playerResource.GemCount++;
            _inGamePanelController.SetGemCountTxt();
        }

        private void OnCompletedCollecting(Transform collectable, CollectableType collectableType)
        {
            HapticManager.PlayHaptic(HapticType.SoftImpact);

            collectable.gameObject.SetActive(false);

        }

        private void SetActivity(bool value)
        {
            _eneble = value;
            if (!value)
            {
                _target = null;
                _playerMovementController.Target = null;
                _playerMovementController.IsCloseEnemyFound = false;
                IsEnemyFound = false;
                _craftable = null;
                _enemy = null;
                _enemyDetector.ClearDeatected();
                _craftDetector.ClearDeatected();

            }
            //    _botController.Enable = value;
        }



    }


}