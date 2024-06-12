using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Craft;
using _Project.Scripts.Interactable.Craft;
using Codice.Client.BaseCommands.Import;
using UnityEngine;
using VContainer;
namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class DetectionController : IEnemyDetector, ICraftDetectorReciver
    {
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private BotController _botController;
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
    }
}