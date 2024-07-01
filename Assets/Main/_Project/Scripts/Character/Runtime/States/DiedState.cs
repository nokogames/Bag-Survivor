using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Project.Scripts.Character.Runtime.Controllers;
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
        public void Initialize()
        {

        }
        public void Enter()
        {
            _detectionController.Enable = false;
            _botController.PlaceBots();
            _characterGraphics.DisableIK();
            _gunBehavior.SetActivity(false);

            _panelController.PlayerDied();

        }

        public void Exit()
        {
              _detectionController.Enable =true;
        }

        public void FixedTick()
        {
        }

        public void Tick()
        {
 
        }
    }
}