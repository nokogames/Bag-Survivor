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
        private PlayerSM _playerSM;
        private IEnemy _targetEnemy;
        public IEnemy TargetEnemy => _targetEnemy;
        private ICraftable _carftable;
        public ICraftable Craftable => _carftable;

        public void Initialise(PlayerSM playerSM)
        {
            _playerSM = playerSM;

        }
        public void OnCraftableDetect(ICraftable crrCraftable)
        {
            _carftable = crrCraftable;
            ChangeStateToCraft();
        }

        private void ChangeStateToCraft()
        {
            if (_targetEnemy != null || _carftable == null || _carftable.CanCraftable) return;
            _playerSM.ChangeState(_playerSM.CraftState);

        }

        public void OnEnemyDetected(IEnemy detectedEnemyInfo)
        {

            _targetEnemy = detectedEnemyInfo;
            // _isCloseEnemyFound = detectedEnemyInfo != null;

            _playerMovementController.TargetEnemy = detectedEnemyInfo;
            _playerMovementController.IsCloseEnemyFound = detectedEnemyInfo != null;
            if (detectedEnemyInfo == null) ChangeStateToCraft();


        }
    }
}