using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Character.Runtime.Controllers
{
    public class DetectionController : IEnemyDetector
    {
        [Inject] private PlayerMovementController _playerMovementController;

        private IEnemy _targetEnemy;
        public IEnemy TargetEnemy => _targetEnemy;
        public void OnEnemyDetected(IEnemy detectedEnemyInfo)
        {   
            
            _targetEnemy = detectedEnemyInfo;
            // _isCloseEnemyFound = detectedEnemyInfo != null;

            _playerMovementController.TargetEnemy = detectedEnemyInfo;
            _playerMovementController.IsCloseEnemyFound = detectedEnemyInfo != null;


        }
    }
}