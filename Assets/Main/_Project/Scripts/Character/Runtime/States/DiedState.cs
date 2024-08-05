using System;
using _Project.Scripts.Character.EnemyRuntime;
using _Project.Scripts.Character.Runtime.Controllers;
using _Project.Scripts.Level;
using _Project.Scripts.Reusable;
using _Project.Scripts.UI.Controllers;
using VContainer;

namespace _Project.Scripts.Character.Runtime.States
{
    public class DiedState : IState
    {
        [Inject] private BaseGunBehavior _gunBehavior;
        [Inject] private PlayerMovementController _playerMovementController;
        [Inject] private BotController _botController;
        [Inject] private CharacterGraphics _characterGraphics;
        [Inject] private PanelController _panelController;
        [Inject] private DetectionController _detectionController;
        private EnemyManager _enemyManager;
        [Inject] private LevelDataManager _levelDataManager;
        //  [Inject] private InLevelEvents _inLevelEvents;
        public void Initialize(EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
        }
        public void Enter()
        {
            _detectionController.Enable = false;
            _playerMovementController.PlayerIsDeath = true;

            _botController.PlaceBots();
            _characterGraphics.DisableIK();
            _gunBehavior.SetActivity(false);

            _panelController.PlayerDied();
            _enemyManager.PlayerDied();
            _levelDataManager.PlayerDied();

        }

        public void Exit()
        {
            _detectionController.Enable = true;
            _characterGraphics.EnableIK();
            _gunBehavior.SetActivity(true);
            _playerMovementController.PlayerIsDeath = false;
        }

        public void FixedTick()
        {
        }

        public void Tick()
        {

        }
    }
}